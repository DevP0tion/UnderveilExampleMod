using System.Collections.Generic;
using UnderveilModLoader;

namespace UnderveilExampleMod.Events
{
    // assets/lang/<locale>/shrine.json
    [Register("shrine")]
    internal sealed class ExampleEvent : ModEvent
    {
        private const int BloodHpCost = 5;
        private const int BloodGold = 50;
        private const int GoldCost = 30;
        private const int GoldMaxHp = 3;

        protected override List<EventChoice> CreateChoices()
        {
            return new List<EventChoice>
            {
                new EventChoice
                {
                    choiceText = Text("choice.blood").Format(BloodHpCost.ToString(), BloodGold.ToString()),
                    requiredHp = BloodHpCost + 1,
                    outcomes = new List<EventOutcome>
                    {
                        new EventOutcome { type = EventOutcome.Type.LoseHp, value = BloodHpCost },
                        new EventOutcome { type = EventOutcome.Type.GainGold, value = BloodGold, resultText = Text("result.blood") }
                    }
                },
                new EventChoice
                {
                    choiceText = Text("choice.gold").Format(GoldCost.ToString(), GoldMaxHp.ToString()),
                    requiredGold = GoldCost,
                    outcomes = new List<EventOutcome>
                    {
                        new EventOutcome { type = EventOutcome.Type.LoseGold, value = GoldCost },
                        new EventOutcome { type = EventOutcome.Type.GainMaxHp, value = GoldMaxHp, resultText = Text("result.gold") }
                    }
                },
                new EventChoice
                {
                    choiceText = Text("choice.leave"),
                    outcomes = new List<EventOutcome>
                    {
                        new EventOutcome { type = EventOutcome.Type.None, resultText = Text("result.leave") }
                    }
                }
            };
        }
    }
}
