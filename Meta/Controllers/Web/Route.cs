namespace TowerDefence
{
    public class Route
    {
        public string url;
        public string all_cards;
        public string player_cards;    
        public string shop;
        public string shop_buy_bunner;
        public string achievements;
        
        public Route(   string url = "game.aivanof.ru/api/",
                        string all_cards = "all_cards", 
                        string player_cards = "player_cards",
                        string shop = "shop", 
                        string shop_buy_bunner = @"shop/buy/",
                        string achievements = "achievements")
        {
            this.url = url;
            this.all_cards = all_cards;
            this.player_cards = player_cards;
            this.shop = shop;
            this.shop_buy_bunner = shop_buy_bunner;
            this.achievements = achievements;
        }
    }
}
