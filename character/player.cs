namespace Character {
    public class Player {
        public string name {get; set;}
        public Attributes attributes{get; set;}

        public Player(string name) {
            this.name = name;
            this.attributes = new Attributes();
        }

        public string details() {
            return string.Format("Name: {0}\t{1}", this.name, this.attributes.details());
        }
    }
}