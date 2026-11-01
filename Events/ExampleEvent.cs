using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnderveilModLoader;
using UnityEngine;

namespace UnderveilExampleMod.Events
{
    internal static class ExampleEvent
    {
        // Far above the game's own event ids.
        private const int EventId = 90001;

        // {mod id}.{file name}.{key} from assets/lang/<locale>/shrine.json
        private const string Text = "UnderveilExampleMod.shrine.";

        private const int BloodHpCost = 5;
        private const int BloodGold = 50;
        private const int GoldCost = 30;
        private const int GoldMaxHp = 3;

        internal static EventData Create(Sprite artwork)
        {
            EventData data = ScriptableObject.CreateInstance<EventData>();
            data.eventId = EventId;
            data.eventName = Text + "name";
            data.artwork = artwork;
            data.description = Text + "description";
            data.choices = new List<EventChoice>
            {
                new EventChoice
                {
                    choiceText = (Text + "choice.blood").Format(BloodHpCost.ToString(), BloodGold.ToString()),
                    requiredHp = BloodHpCost + 1,
                    outcomes = new List<EventOutcome>
                    {
                        new EventOutcome { type = EventOutcome.Type.LoseHp, value = BloodHpCost },
                        new EventOutcome { type = EventOutcome.Type.GainGold, value = BloodGold, resultText = Text + "result.blood" }
                    }
                },
                new EventChoice
                {
                    choiceText = (Text + "choice.gold").Format(GoldCost.ToString(), GoldMaxHp.ToString()),
                    requiredGold = GoldCost,
                    outcomes = new List<EventOutcome>
                    {
                        new EventOutcome { type = EventOutcome.Type.LoseGold, value = GoldCost },
                        new EventOutcome { type = EventOutcome.Type.GainMaxHp, value = GoldMaxHp, resultText = Text + "result.gold" }
                    }
                },
                new EventChoice
                {
                    choiceText = Text + "choice.leave",
                    outcomes = new List<EventOutcome>
                    {
                        new EventOutcome { type = EventOutcome.Type.None, resultText = Text + "result.leave" }
                    }
                }
            };
            return data;
        }
    }

    [HarmonyPatch(typeof(EventManager), "Awake")]
    internal static class EventManagerAwakePatch
    {
        private static void Postfix(List<EventData> ___allEvents)
        {
            // Borrows artwork from a game event because the mod has no sprite of its own.
            Sprite artwork = ___allEvents.Select(e => e.artwork).First(a => a != null);
            ___allEvents.Add(ExampleEvent.Create(artwork));
            UnderveilExampleMod.Log.LogInfo($"Registered example event ({___allEvents.Count} events in pool)");
        }
    }
}
