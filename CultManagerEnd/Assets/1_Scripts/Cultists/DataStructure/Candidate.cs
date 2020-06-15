using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CultManager
{
    [System.Serializable]
    public class Candidate
    {
        public Cultist cultist { get; private set; }
        public int policeValue { get; private set; }
        public int moneyValue { get; private set; }
        public string description { get; private set; }

        public Candidate(Cultist _cultist, int _policeValue, int _moneyValue)
        {
            cultist = _cultist;
            policeValue = _policeValue;
            moneyValue = _moneyValue;
            description = RandomDecription();
        }

        private string RandomDecription()
        {
            List<string> sentences = new List<string>();

            sentences.Add("Likes writing about pickles and share it online and nobody cares.");
            sentences.Add("Wants to be a fisherman when becoming older but doesn't know how to hold a fishing rod.");
            sentences.Add("Can cram for tests, but can’t remember which one he is supposed to.");
            sentences.Add("Eats before thinking and then, eats again because he sure likes to eat.");
            sentences.Add("Often go in front of a crowd in public place for yo-yoing.");
            sentences.Add("Is a professional poker player without cheating which makes him pretty famous.");
            sentences.Add("Can remember stuff from one minute ago, and will brag about it 5 minutes more.");
            sentences.Add("can do handstands, but only in presence of a group of cute girls.");
            sentences.Add("Oversleeps when anxious, then starts being anxious because he overslept.");
            sentences.Add("Can wake up early if Mom has cooked some delicious food.");
            sentences.Add("Makes the best turkey of the valley when it’s Thanksgiving.");
            sentences.Add("Is a professional playbacker, but only performs alone in his room.");
            sentences.Add("His skill is making budgets for 25 year laters. He won't live 25 years later.");
            sentences.Add("Was a professional apple peeler in a famous restaurant.");
            sentences.Add("Has an indescriptible passion for skimming books.");
            sentences.Add("Does like trains.");
            sentences.Add("Can eat competitively without feeling ill for 10 minutes.");
            sentences.Add("Can draw perfect circle with the tiniest pencil.");
            sentences.Add("Makes the best back flipping in town.");
            sentences.Add("Can twist his tongue and still speaks normally after that.");
            sentences.Add(" Is a reformed toxic player from competitive video games.");
            sentences.Add("Can stay awake, but he is awake really?");
            sentences.Add("Talks to bugs, because bugs are better people than people.");
            sentences.Add("Can cook delicious and spicy guacamole learnt from his Grandma.");
            sentences.Add("Looks like a shy person but is secretly a rap god.");
            sentences.Add("Can drive any kind of tractors without his hands on the wheel.");
            sentences.Add("Can’t stay five minutes in front of a crowd without having a heart attack.");
            sentences.Add("Is the most beautiful person in his village of five persons. ");
            sentences.Add("Doesn't wear any clothes unless it’s written ‘’I love Charts’’.");
            sentences.Add("Has a secret fear of bleeding and doesn’t know why he is here.");
            sentences.Add("Is never gonna give you up and never gonna let you down.");
            sentences.Add("Can speak 25 languages including the flower language which is pretty impressive.");
            sentences.Add("Has quit school at young age to become a world famous mental math champion.");
            sentences.Add("Used to draw people die when was younger. Is now reformed and only draw happy things.");
            sentences.Add("Campaigns for pinguins rights. Is capable of writing things on a wall to spread his message");
            sentences.Add("Can watch a romantic movie without crying, but can’t watch two of them in a round.");
            sentences.Add("Has done a speedrun of mario 64, 64 times but always loses at the final boss. ");
            sentences.Add("Likes telling to everyone that Interstellar is not a scientifically accurate movie.");
            sentences.Add("Can read backward really quickly but nobody understands him.");

            string result = "";
            for (int i = 0; i < 2; i++)
            {
                int rdm = Random.Range(0, sentences.Count);
                result += sentences[rdm]+'\n';
                sentences.RemoveAt(rdm);
            }

            return result;
        }
    }
}

