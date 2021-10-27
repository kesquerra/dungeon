namespace Character {
    public class Player {
        public string name {get; set;}
        public int level {get; set;}
        public int health {get; set;}
        public double mana {get; set;}
        public Attributes attributes{get; set;}

        public Player(string name) {
            this.name = name;
            this.level = 1;
            this.health = 50;
            this.mana = 15;
            this.attributes = new Attributes();
        }

        public string details() {
            return string.Format(
                "Char: {0}\tLevel: {1}\tHealth: {2}\tMana: {3}\n" +
                "Str: {4}\t\tDex: {5}\t\tVit: {6}\t\tEne: {7}\t\tSta: {8}", 
                this.name, this.level, this.health, this.mana,
                this.attributes.str, this.attributes.dex, this.attributes.vit, this.attributes.ene, this.attributes.sta);
        } 
    }
}