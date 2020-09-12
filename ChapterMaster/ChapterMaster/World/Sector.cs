using System;
using System.Collections.Generic;
using System.Diagnostics;
using ChapterMaster.Util;

namespace ChapterMaster.World
{
    public class Sector
    {
        public List<System> Systems = new List<System>();
        // static organization of lane graph
        public List<WarpLane> WarpLanes = new List<WarpLane>(); // like this?
        public List<Fleet.Fleet> Fleets = new List<Fleet.Fleet>();
        public Dictionary<string, Faction.Faction> Factions = new Dictionary<string, Faction.Faction>();

        int Turn;
        #region Generation
        public Random random;
        #region SystemNames
        public List<string> SystemNames = new List<string>(){"Terak",
   "Roma",
   "Noctae",
   "Piscium",
   "Illan",
   "Xi-He",
   "Carinae",
   "Skuse",
   "Voltantis",
   "Vidi",
   "Hasta",
   "Dagon",
   "Pocki",
   "Resheph",
   "Hemera",
   "Iman",
   "Chorta",
   "Atlanta",
   "Lyncis",
   "Modgud",
   "Oynyena",
   "Onian",
   "Helen",
   "Canum",
   "Qetesh",
   "Skonii",
   "Lytir",
   "Corvi",
   "Yogneek",
   "Delphini",
   "Freya",
   "Gaima",
   "Vespae",
   "Endiku",
   "Menthu",
   "Elyon",
   "Gone",
   "Baxu",
   "Maghda",
   "Leporis",
   "Tiamat",
   "Ceti",
   "Atep",
   "Amon",
   "Asherah",
   "Xu Xiu",
   "Kim Jong",
   "Gonj Mik",
   "Zentra",
   "Azeroth",
   "Morphua",
   "Muric",
   "Sextanis",
   "Betelgeuse",
   "Betechton",
   "Soachton",
   "Ao-Chin",
   "Hordi",
   "Crucis",
   "Lustania",
   "Albion",
   "Bongistan",
   "Natsigan",
   "Orwell",
   "Dagobah",
   "Haboga",
   "Outer Heaven",
   "Sodden Hollow",
   "Protasia",
   "Veneria",
   "Iocanthus",
   "Quaddis",
   "Belahaam",
   "Scarric",
   "Gelmito",
   "Josian",
   "Turanshush",
   "Balecaster",
   "Belacane",
   "Avitohol",
   "Brassica",
   "Sinophia",
   "Skyren",
   "Antioch",
   "Balanor",
   "Cathox",
   "Mahr'douk",
   "Mordax",
   "Phall",
   "Vilhadran",
   "Chaeros",
   "Yaymar",
   "Orax",
   "Laurentix",
   "Belisimar",
   "Vigilatum",
   "Korolis",
   "Stryken",
   "Tephaine",
   "Chinchare",
   "Galathamar",
   "Lordran",
   "Thasia",
   "Lycosidae",
   "Purgatrex",
   "Sabbatorus",
   "Lysades",
   "Jerulas",
   "Ornsworld",
   "Treconandal",
   "Jubal",
   "Mordian",
   "Khardeph",
   "Anticanis",
   "Landersund",
   "Aquasulis",
   "Uristes",
   "Euphrate",
   "Menazoid",
   "Jaego",
   "Canemara",
   "Seadelant",
   "Ostrola",
   "Ilbira",
   "Mard",
   "Carshim",
   "Addolorata",
   "Dolsene",
   "Bolanion",
   "Solveig",
   "Frengold",
   "Urdesh",
   "Fornax",
   "Cociaminus",
   "Verghast",
   "Ancreon",
   "Amedeo",
   "Armatura",
   "Nuceria",
   "Konor",
   "Isstvan",
   "Paramar",
   "Bucephalon",
   "Ghourra",
   "Kerondys",
   "Lyubov",
   "Parthenope",
   "Rosangela",
   "Sapiencia",
   "Summaminus",
   "Taliscant",
   "Stalinvast",
   "Denova",
   "Tibrias",
   "Polonus",
   "Petrostock",
   "Tunusk",
   "Venady",
   "Voltemand",
   "Anark Zeta",
   "Atar-Median",
   "Cabulis",
   "Enceladus",
   "Sarum",
   "Incaldion",
   "M'khand",
   "Sabatine",
   "Orestes",
   "Stygies",
   "Vasalius",
   "Vordrast",
   "Boonhaven",
   "Barbarus",
   "Mandragoran",
   "Anphelion",
   "Solstice",
   "Inwit",
   "Magdelene",
   "Ghorstangrad",
   "Kastorel",
   "Borsis",
   "Volistad",
   "Mortant",
   "Ogrolla",
   "Tallarax",
   "Viridios",
   "Somonor",
   "Urphir",
   "Loki",
   "Varsavus",
   "Vagorn",
   "Zeist",
   "Sigilare",
   "Aeschylrai",
   "Accatran",
   "Metalica",
   "Kolossi",
   "Espandor",
   "Voltoris",
   "Draconis",
   "Luxor",
   "Au'taal",
   "Badab",
   "Alaric",
   "Yithic",
   "Tolkhan",
   "Crematis",
   "Pixor",
   "Bellephon",
   "Boucherin",
   "Chemos",
   "Corinthe",
   "Corrinos",
   "Cytheria",
   "Damnos",
   "Davin",
   "Drisinta",
   "Dynathi",
   "Eorcshia",
   "Gardinaal",
   "Fergax",
   "Taros",
   "Garevo",
   "Blasted Heath",
   "Haletho",
   "Hammeront",
   "Honourum",
   "Nidus Diptera",
   "Optera",
   "Menopetra",
   "Jjojos",
   "Kaelas",
   "Ichar",
   "Jhanna",
   "Incaladion",
   "Iolac",
   "Krastellan",
   "Lunaphage",
   "Memlok",
   "Arradin",
   "M'khan",
   "Miral",
   "Neverlight",
   "Moloch",
   "Tatarstia",
   "Veracia",
   "New Veracia",
   "New Varsavus",
   "New Petrostock",
   "New Thasia",
   "Lohab",
   "Lalam",
   "Lowam",
   "Obsidia",
   "Nihilas",
   "Molech",
   "Olympia",
   "Pavonis",
   "Providence",
   "Radnar",
   "Sotha",
   "Sulis",
   "Niflheim",
   "Schindelgheist",
   "Jotunheim",
   "Tarturga",
   "Ursidhe-Ka",
   "Zathatethus",
   "Hynnes",
   "Ammonai",
   "Dirge",
   "Nexaris",
   "Circe",
   "Kallas",
   "Jursa",
   "Parmenio",
   "Quintarn",
   "Tycor",
   "Masali",
   "Mordan",
   "Frankonia",
   "Maesa",
   "Laconia",
   "Nicaea",
   "Hellsiris",
   "Apollonia",
   "Naogeddon",
   "Sarcosa ",
   "Tanhaus",
   "Athelaq",
   "K'phra",
   "Athelaq",
   "Tyrannis",
   "Solitude",
   "Skgorria",
   "Atopiana",
   "Hataria",
   "Boneyard",
   "Hypnoth",
   "Hydraphur",
   "Barvaria",
   "Doranno",
   "Drathorian",
   "Kraeg",
   "Graia",
   "Minerva",
   "Gehenna",
   "Mortarius",
   "Zindleschlitz",
   "Acteron",
   "Quintox",
   "Dreadhaven",
   "Pandorax",
   "Pythos",
   "Thandros",
   "Ymgarl",
   "Raeden",
   "Koralkal",
   "Zorastra",
   "Hagia",
   "Knowhere",
   "Mattiax",
   "Wardian",
   "Airephal",
   "Xagem",
   "Cegorachi",
   "Jomungandr",
   "Nostramo",
   "Stygia",
   "Incanda",
   "Haeraphya",
   "Lycaeum",
   "Contqual",
   "Gorro",
   "Flint",
   "Deus",
   "Persya",
   "Heloeum",
   "Devilus",
   "Agathon",
   "Anvilus",
   "Vostroya",
   "Groznyj Grad",
   "Artaxerxes",
   "Solania",
   "Barathred",
   "Desperation",
   "Fargotia",
   "New Aiur",
   "New Stalinvast",
   "Beroghast",
   "Heilog’s Star",
   "Azghrax",
   "Coriolanthe",
   "Krumpville",
   "Dimmamak",
   "Primordial Frost",
   "Mordax Prime",
   "Angelus",
   "Ephrath",
   "Theboze",
   "Urmox",
   "Felcarn",
   "Mathog",
   "Forrax",
   "Ganymethia",
   "Ghenna",
   "Bodt",
   "Haringvleet",
   "Hollonan",
   "Hexxo",
   "Hadriath",
   "Ironholm",
   "Hamilcar",
   "Kartheope",
   "Canis Canem",
   "Ksatella",
   "Magdellan",
   "Manticore",
   "Malodrax",
   "Castellax",
   "Thanatar",
   "Sarcosa",
   "Penumbra",
   "Siriua",
   "Impetus",
   "Bretonia",
   "Lesser Mantelius",
   "Botmur",
   "Signus Prime",
   "Quintaine",
   "Oranos",
   "Hyperion",
   "Leto",
   "Pervigilium",
   "Vergilian",
   "Zuerlais",
   "Ullanor",
   "Shadrac",
   "Sondheim",
   "Rostern",
   "Protheus",
   "Kronos",
   "Tartarus",
   "Portenus",
   "Borealum",
   "New Tanith",
   "Red Reach",
   "Bella",
   "Vaxhallia",
   "Varestus",
   "Gangrenous Rot",
   "Yaogeddon",
   "Ygetheddon",
   "Yoggoth",
   "Jagga",
   "Indra-sul",
   "Dregeddon",
   "Scarus",
   "Vieglehaven",
   "Callistus",
   "Majorial",
   "Cerastus",
   "Venator",
   "Macharia",
   "Loikik",
   "Erasmus",
   "Eskarne",
   "Dymphna",
   "Moritia Prime",
   "Borisia",
   "Carthage",
   "Suphera",
   "Ghourra",
   "Califor",
   "Tarant",
   "Chanicia",
   "Herodor",
   "Heskeloth",
   "Mehitabel",
   "Presarius",
   "Obermid",
   "New Tarant",
   "Pachuco",
   "Voltemand",
   "Bojana",
   "Gorgonia",
   "Pintax",
   "Xatill",
   "Nefalia",
   "Polmuss",
   "New Carthage",
   "Forsarr",
   "Capilene",
   "Minisotira",
   "Deneb",
   "Thea",
   "Ando",
   "Iapetus",
   "Klimt",
   "Astrakhan",
   "Atlas",
   "Veles",
   "Pannonia",
   "Murom",
   "Khorinis",
   "Endymion",
   "Vall Major",
   "Praste",
   "Korabaellan",
   "Korvaran",
   "Muoskaerl",
   "Alteraan",
   "Veritas",
   "Telenor",
   "Selene",
   "Climaxus",
   "Corkanium",
   "Peripheris",
   "Intarme",
   "Quintus Superior",
   "Zaporozhye",
   "Pontus",
   "Haliphax",
   "Perun",
   "Judean",
   "Felisian",
   "Nirn",
   "Biik",
   "Starrym",
   "Morrowynd",
   "Tintangiel",
   "Kuhrwax",
   "Avalon",
   "Vyndyalii",
   "Zaphonia",
   "Zinerra",
   "Ullatarin",
   "Vaelis",
   "Chimaera",
   "Arkhamis",
   "Shadow Hearth",
   "Naeraea",
   "Pandora",
   "Lorvarian",
   "Nova Terra",
   "Roserias",
   "Rybiern",
   "Fastoon",
   "Belden",
   "Velden",
   "Grenada",
   "Raiken",
   "Koros",
   "Toledo",
   "Valyria",
   "Dead Cell",
   "Ghis",
   "Canukistan",
   "Gyratio",
   "Fistiox",
   "Adolphian",
   "Quarth",
   "Dornus Noangulus",
   "Dornari",
   "Vandiria",
   "Pearia",
   "Sheol",
   "Libertania",
   "Woden",
   "Guderian",
   "Edelweiss",
   "Gotenland",
   "Theodorichshaven",
   "Batoria",
   "Husania",
   "Urslavik",
   "Creedia",
   "Ulfa",
   "Amerigo",
   "Tilfis",
   "Rovno",
   "Reno",
   "Constantinopolis",
   "Istanpulia",
   "Inuit",
   "Grave",
   "Vardenfeld",
   "Scorched Citadel",
   "Hammerfront",
   "Huldwynia",
   "Brabastis",
   "Diherim",
   "Yhette",
   "Retsam Retpahc",
   "Black Creek",
   "Tungusta",
   "Pugio",
   "Yavin",
   "Kup Teraz",
   "Serenity",
   "Kurimizon",
   "Tuskus",
   "Whitefall",
   "Zalia",
   "Regina"};
        #endregion
        public void Prepare()
        {
            random = new Random(41); //220
        }
        #region Unused Generator Code
        /*
        int Clusters = 0;
        int lastClusterX = 0;
        int lastClusterY = 0;
        public void Generate()
        {
            int lastX = 0;
            int lastY = 0;
            for (int localSystem = 0; localSystem < 4; localSystem++)
            {
                if (random.Next(10) > 1)
                {
                    int x = lastX + random.Next(500) + 100;
                    int y = lastY + random.Next(300) + 100;
                    bool go = true;
                    foreach (System sys in Systems)
                    {
                        if (RoundedDistance(sys.x, sys.y, x, y) < 100) { go = false; }
                    }
                    if (go)
                    {
                        Systems.Add(new System(random.Next(6), x % 1280, y % 960));
                        lastX = x;
                        lastY = y;
                    }
                    continue;
                }
                else
                {
                    if (Clusters < 100)
                    {
                        //if (RoundedDistance(lastClusterX, lastClusterY, lastX, lastY) < 200) { return; }
                        Generate();
                        Clusters++;
                        lastClusterX = lastX;
                        lastClusterY = lastY;
                    }
                    else
                    {
                        if (Clusters > 50)
                        {
                            Debug.WriteLine("Reached Cluster Limit in Generation.");
                            return;
                        }
                    }
                }
            }
        }
        public void Generate(int number) // still doesn't work.
        {
            //Systems.Add(new System(random.Next(6), random.Next(1280), random.Next(960)));
            while (Systems.Count < number)
            {
                foreach (System sys in Systems)
                {
                    int x = random.Next(1280);
                    int y = random.Next(960);
                    if (RoundedDistance(sys.x, sys.y, x, y) > 100) Systems.Add(new System(random.Next(6), x, y));
                    //Generate(number);
                }
            }
        }


        int min = 30;
        int max = 100;
        public void NormalDistGenerate()
        {
            int count = (int)Math.Round(random.NormallyDistributedSingle(15, 60, min, max));
            int sprawl = 500;
            int average = 150;
            int lastX = 500;
            int lastY = 500;

            for (int i = 0; i < count; i++)
            {
                int x = (int)Math.Round(random.NormallyDistributedSingle(sprawl, average, -500, 500));
                int y = (int)Math.Round(random.NormallyDistributedSingle(sprawl, average, -300, 300)); ;
                int xBorder = 1280 - x;
                int yBorder = 960 - x;
                if (xBorder >= 0 && xBorder < 300 && random.Next(2) == 1)
                {
                    x = 1280 - x;
                }
                if (yBorder >= 0 && yBorder < 200 && random.Next(2) == 1)
                {
                    y = 960 - y;
                }
                Systems.Add(new System(random.Next(6),
                            MathUtil.ClampInt(lastX + x, 0, 1280),
                            MathUtil.ClampInt(lastY + y, 0, 960)));
                lastX = MathUtil.ClampInt(lastX + x, 0, 1280);
                lastY = MathUtil.ClampInt(lastY + y, 0, 960);
            }
            //lastX = lastY = 0;
        }
        */
        #endregion
        public void GridGenerate(int clusterNo, int minDistance, int clusterSize, int width, int height)
        {
            Systems.Clear();
            for (int x = 0; x < width / clusterSize; x++)
            {
                for (int y = 0; y < height / clusterSize; y++)
                {
                    int no = Systems.Count;
                    if (no < clusterNo)
                    {
                        int newX = x * clusterSize + (int)Math.Round(
                            random.NormallyDistributedSingle(width / 2, width / 4, -width / 2, width / 2)); // skew to the east
                        int newY = y * clusterSize + (int)Math.Round(
                            random.NormallyDistributedSingle(height / 2, height / 4, -height / 2, height / 2)); // skew to the south
                        if (no > 0)
                        {
                            for (int secondCircle = 0; secondCircle < no; secondCircle++) // hell loop
                            {
                                foreach (System s in Systems)
                                {
                                    int dis = MathUtil.RoundedDistance(s.x, s.y, newX, newY);
                                    if (dis < minDistance) // skew away from other stars
                                    {
                                        newX += (int)Math.Round(random.NormallyDistributedSingle(dis, 0, -2 * dis, 2 * dis)); // add 100 or something, idk what to do here
                                        newY += (int)Math.Round(random.NormallyDistributedSingle(dis, 0, -2 * dis, 2 * dis)); // add 100 or something, idk what to do here
                                    }
                                }
                            }
                        }
                        if (newX < 0) newX += width; // randomize the new posiiton
                        if (newY < 0) newY += height; // randomize the new position
                        bool forgeProne = false;
                        if (random.Next(2) == 1 || newX > width) // sprawl out to the west
                        {
                            newX = width - newX;
                            forgeProne = true;
                        }
                        if (random.Next(2) == 1 || newY > height) // sprawl out to the north
                        {
                            newY = height - newY;
                            forgeProne = true;
                        }
                        System system = new System(random.Next(6), newX, newY);
                        system.forgeProne = forgeProne;
                        Systems.Add(new System(random.Next(6), newX, newY));
                    }
                }
            }
        }
        //Pain.
        public void WarpLaneGenerate()
        {
            WarpLanes.Clear();
            for (int system = 0; system < Systems.Count; system++)
            {
                for (int other = 0; other < Systems.Count; other++)
                {
                    if (system == other) // system is other
                    {
                        // ignore
                        continue;
                    }
                    else
                    {
                        int distance = MathUtil.RoundedDistance(
                            Systems[system].x, Systems[system].y,
                            Systems[other].x, Systems[other].y);
                        if (Systems[system].numberOfLanes < 3 && Systems[other].numberOfLanes < 3)
                        {
                            // balance random factors
                            if (distance < 100 && random.Next(60) > 7)
                            {
                                WarpLanes.Add(new WarpLane(system, other));
                                Systems[system].numberOfLanes++;
                                Systems[other].numberOfLanes++;
                            }
                            else if (distance < 200 && random.Next(125) < 5)
                            {
                                WarpLanes.Add(new WarpLane(system, other));
                                Systems[system].numberOfLanes++;
                                Systems[other].numberOfLanes++;
                            }
                            else if (distance < 300 && random.Next(200) < 7)
                            {
                                WarpLanes.Add(new WarpLane(system, other));
                                Systems[system].numberOfLanes++;
                                Systems[other].numberOfLanes++;
                            }
                            else if (distance < 400 && random.Next(250) < 5)
                            {
                                WarpLanes.Add(new WarpLane(system, other));
                                Systems[system].numberOfLanes++;
                                Systems[other].numberOfLanes++;
                            }
                        }
                    }
                }
            }
        }
        public void GenerateSystemNames()
        {
            // https://stackoverflow.com/questions/273313/randomize-a-listt Fisher-Yates shuffle
            int i = SystemNames.Count;
            while (i > 1)
            {
                i--;
                int k = random.Next(i + 1);
                string value = SystemNames[k];
                SystemNames[k] = SystemNames[i];
                SystemNames[i] = value;
            }
            for (int n = 0; n < Systems.Count; n++)
            {
                Systems[n].name = SystemNames[n];
            }
        }
        int forgeMinimum = 4;
        int forgeMaximum = 10; // TODO: implement
        int noForge = 0;
        public List<Type> Types;
        public void GeneratePlanets()
        {

            for (int n = 0; n < Systems.Count; n++)
            {
                Systems[n].id = n; // please don't judge me for this
                int numberOfPlanets = (int)Math.Ceiling(MathUtil.NormallyDistributedSingle(random, 2, 2, 0.1f, 4));
                int numberOfForge = 0;
                if (noForge < forgeMinimum)
                {
                    if (Systems[n].forgeProne)
                    {
                        Systems[n].Planets.Add(new Planet(Type.FORGE, n, numberOfForge));
                        numberOfForge++;
                        noForge++;
                    }
                }
                for (int nPlanet = numberOfForge; nPlanet < numberOfPlanets; nPlanet++)
                {
                    int type = random.Next(16);

                    if (type == (int)Type.FORGE) noForge++;
                    if (type != 7)
                        Systems[n].Planets.Add(new Planet((Type)type, n, nPlanet));
                }

            }
        }
        #endregion
        public int CalculateTravelTurns(Fleet.Fleet fleet)
        {
            int distance = MathUtil.RoundedDistance(Systems[fleet.originSystemId].x, Systems[fleet.originSystemId].y, Systems[fleet.destinationSystemId].x, Systems[fleet.destinationSystemId].y);
            foreach (WarpLane warpLane in WarpLanes)
            {

                if ((warpLane.systemId1 == fleet.originSystemId || warpLane.systemId1 == fleet.destinationSystemId)
                 && (warpLane.systemId2 == fleet.destinationSystemId || warpLane.systemId2 == fleet.originSystemId))
                {
                    return (distance / (fleet.fleetSpeed * 3)) + 1; // haha, this prevented short warp lanes from making fleets disappear when eta reaches 0
                }
            }
            return (distance / fleet.fleetSpeed) + 1;
        }
        public void TurnUpdate()
        {
            foreach (System system in Systems)
            {
                //system.Update(this);
            }
            foreach (Fleet.Fleet fleet in Fleets)
            {
                fleet.Update(this);
            }
            Turn++;
            Systems[0].Planets[0].FactionOwner = "Orks";
            foreach (KeyValuePair<string, float> DasKapital in Systems[0].FindOwners())
            {
                Debug.WriteLine($"Faction : {DasKapital.Key}, Control : {DasKapital.Value}");
            }
        }

        public string GetImperialDate()
        {
            int millenium = 42;
            int year = 000;
            int yearfraction = (50 * Turn) + 1;
            if (yearfraction >= 1000)
            {
                year = (yearfraction) / 1000;
                if (yearfraction - 1000 * year == 000)
                {
                    yearfraction = 1;
                }
                else
                {
                    yearfraction = yearfraction % 1000;
                }
            }
            return $"5 {yearfraction.ToString("D3")} {year.ToString("D3")} M{millenium}";
        }

    }
}
