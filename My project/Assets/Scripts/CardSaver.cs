using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class CardSaver
{

    private static bool has = false;

    public static void Init()
    {
        if (has) return;
        has = true;
        if (!File.Exists(Application.persistentDataPath + "/cards.csv"))
        {

            TextAsset file = Resources.Load<TextAsset>("Cards/cards");

            FileStream stream = new FileStream(Application.persistentDataPath + "/cards.csv", FileMode.Create);
            byte[] b = Encoding.UTF8.GetBytes(file.text);
            stream.Write(b, 0, b.Length);

            stream.Close();
        }
        Debug.Log(Application.persistentDataPath);
        FileStream streamm = new FileStream(Application.persistentDataPath + "/cards.csv", FileMode.Open);

        StreamReader reader = new StreamReader(streamm);

        List<CardInfo> cards = new List<CardInfo>();

        int Ensure(string a)
        {
            Debug.Log(a);
            if (a == null || a.Equals("")) return 0;
            else return int.Parse(a);
        }

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            Debug.Log(line);
            string[] iii = line.Split(',');
            CardInfo info = new CardInfo();
            info.type = iii[0];
            info.amount = Ensure(iii[1]);
            if (iii[0].Equals("Spell"))
            {
                info.spellName = iii[2];
                info.spellDescription = iii[3].Replace("&n", "\n");
            }
            else if (iii[0].Equals("Dungeon"))
            {
                info.heroName = iii[2];
                info.heroDescription = iii[3].Replace("&n", "\n");
                info.heroHealth = Ensure(iii[4]);
                info.heroAttack = Ensure(iii[5]);
                info.heroShield = Ensure(iii[6]);
                info.dungeonName = iii[7];
                info.dungeonDescription = iii[8].Replace("&n", "\n");
                info.dungeonHealth = Ensure(iii[9]);
                info.dungeonAttack = Ensure(iii[10]);
                info.dungeonShield = Ensure(iii[11]);
            }
            else
            {
                info.heroName = iii[2];
                info.heroHealth = Ensure(iii[3]);
                info.heroAttack = Ensure(iii[4]);
                info.heroShield = Ensure(iii[5]);
            }

            cards.Add(info);
        }

        CardManager.LoadCards(cards);
    }

    public static void Save()
    {
        CardInfo[] info = CardManager.GetCards();

        FileStream streamm = new FileStream(Application.persistentDataPath + "/cards.csv", FileMode.Create);

        StreamWriter writer = new StreamWriter(streamm);

        foreach (CardInfo inf in info)
        {
            string line = inf.type + "," + inf.amount + ",";
            if (inf.type == "Spell")
            {
                line += inf.spellName + ",";
                line += inf.spellDescription.Replace("\n", "&n");
            }
            else if (inf.type.Equals("Dungeon"))
            {
                line += inf.heroName + ",";
                line += inf.heroDescription.Replace("\n", "&n") + ",";
                line += inf.heroHealth + ",";
                line += inf.heroAttack + ",";
                line += inf.heroShield + ",";
                line += inf.dungeonName + ",";
                line += inf.dungeonDescription.Replace("\n", "&n") + ",";
                line += inf.dungeonHealth + ",";
                line += inf.dungeonAttack + ",";
                line += inf.dungeonShield;
            }
            else
            {
                line += inf.heroName + ",";
                line += inf.heroHealth + ",";
                line += inf.heroAttack + ",";
                line += inf.heroShield;
            }
            writer.WriteLine(line);
        }
        writer.Close();
    }

}