namespace Character {
    public class Attributes {
        public int str;
        public int dex;
        public int vit;
        public int ene;
        public int sta;

        public Attributes() {
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

        public void level_up() {
            this.sta++;
        }
    }
}