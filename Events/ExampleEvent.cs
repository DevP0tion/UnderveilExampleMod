using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace UnderveilExampleMod.Events
{
    internal static class ExampleEvent
    {
        // Far above the game's own event ids.
        private const int EventId = 90001;

        internal static EventData Create(Sprite artwork)
        {
            EventData data = ScriptableObject.CreateInstance<EventData>();
            data.eventId = EventId;
            // LocalizationHelper returns unknown keys as they are, so plain text works here.
            data.eventName = "Forgotten Shrine";
            data.artwork = artwork;
            data.description = "A small shrine stands by the road. Its bowl is empty, but it seems to be waiting for something.";
            data.choices = new List<EventChoice>
            {
                new EventChoice
                {
                    choiceText = "Offer blood. Lose 5 HP and gain 50 gold.",
                    requiredHp = 6,
                    outcomes = new List<EventOutcome>
                    {
                        new EventOutcome { type = EventOutcome.Type.LoseHp, value = 5 },
                        new EventOutcome { type = EventOutcome.Type.GainGold, value = 50, resultText = "Coins spill from the empty bowl." }
                    }
                },
                new EventChoice
                {
                    choiceText = "Offer gold. Gain 3 max HP.",
                    requiredGold = 30,
                    outcomes = new List<EventOutcome>
                    {
                        new EventOutcome { type = EventOutcome.Type.LoseGold, value = 30 },
                        new EventOutcome { type = EventOutcome.Type.GainMaxHp, value = 3, resultText = "A quiet warmth settles in your chest." }
                    }
                },
                new EventChoice
                {
                    choiceText = "Walk away.",
                    outcomes = new List<EventOutcome>
                    {
                        new EventOutcome { type = EventOutcome.Type.None, resultText = "You leave the shrine undisturbed." }
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
