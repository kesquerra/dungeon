namespace Character {
    public class Attributes {
        public int level;
        public int xp;
        public int health;
        public int mana;
        public int str;
        public int dex;
        public int vit;
        public int ene;
        public int sta;

        public Attributes() {
            this.level = 1;
            this.health = 50;
            this.mana = 15;
            this.str = 20;
            this.dex = 25;
            this.vit = 20;
            this.ene = 15;
            this.sta = 84;
        }

        public void add_energy_point() {
            this.ene++;
        }

        public void add_vitality_point() {
            this.vit++;
            this.sta++;
        }

        public void check_for_level_up() {
            if (this.xp >= 100) {
                this.level_up();
            }
        }

        public void level_up() {
            this.sta++;
            this.health +=2;
        }
        public string details() {
            return string.Format(
                "Level: {0}\tHealth: {1}\tMana: {2}\n" +
                "Str: {3}\t\tDex: {4}\t\tVit: {5}\t\tEne: {6}\t\tSta: {7}", 
                this.level, this.health, this.mana,
                this.str, this.dex, this.vit, this.ene, this.sta);
        } 
    }
}