// Описание меты

Корневой файл (точка запуска):
//Meta.cs
	(monobehaviour, который висит на объекте [Meta] )
В нём происходят запуск даты и всех контроллеров
имеет два поля:
	private DataContainer metaData; - поле, отвечающее за данные игры
	private ControllerContainer controllers; - поле, отвечающее за контроллеры игры

//////////////////////////////////////////////////////////////////////////////////////	
//Данные игры
//////////////////////////////////////////////////////////////////////////////////////	
//Data.cs
(monobehaviour, который висит на дочернем объекте [Meta] -> [MetaData] )(заменяемо)
Он хранит в себе такие поля:
	AllCardsInfo - Данные о всех картах в игре, подгружаемые из allcardsInfo.json
	PlayerCards - данные о картах, открытых ироком, подгружаемые из json
	PlayerDecks - данные о колодах игрока, подгружаемые из json
	Shop - данные о слотах в магазине, подгружаемые из json
	GameCurrency - данные о доступной игроку валюте
***возможно будет дополнено

//////////////////////////////////////////////////////////////////////////////////////		
//Контроллеры
//////////////////////////////////////////////////////////////////////////////////////	
//ControllerContainer.cs
(monobehaviour, который висит на дочернем объекте [Meta] -> [Controllers] )(заменяемо)
Контроллеры являются monobehaviour, реализуют интерфейс IController и вешаются на [Controllers]. 
Их инициализация происходит в момент инициализации ControllerContainer через метод Init(Meta meta), у объекта [Controllers], 
который запускает все свои компоненты-контроллеры через интерфейс IController.

//////////////////////////////////////////////////////////////////////////////////////	
//События
//////////////////////////////////////////////////////////////////////////////////////	
//MetaEvents.cs
все события меты планирую закинуть в этот один файл 
!!! //(возможно это неправильно)
//////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////////////////
//PlayerCardController.cs
Контроллер, отвечающий за действия с картами игрока
имеет поля и методы:
	private Meta _meta - ссылка на мету
	private PlayerCardView view - ссылка на вьюху карт игрока
	private GameObject playerCardPrefab - префаб одной карты
	!!! private TextAsset allCardsInfoAsset - //временный файл json для теста загрузки всех карт
	!!! private TextAsset playerCardsAsset - //временный файл json для теста загрузки карт игрока
	public override void Init( Meta meta) - инициализация, загрузка информации о всех картах и картах игрока,
		подписка на события, Инит вьюхи, спавн карт игрока. //(далее нужно будет вырзать, т.к. получение карт будет после запроса на сервер)
	void spawnPlayerCards() - Спавн карт для текущей колоды во вьюхе
	private void addNewCard( CardInfo cardInfo ) - добавить карту игроку
	private void AddNewCardToView(int cardID, string imageSource) - добавить одну карту во вьюхе
	private void ClearPlayerCards() очистить вьюху
	public void loadDeck(int deckID) загрузить колоду по id
	public void nextDeck() загрузить колоду со следующим id

//PlayerCards.cs  -> class PlayerCards
	public int activeDeckID - активная колода
	public List<PlayerCard> playerCards - список с картами игрока
	public List<PlayerDeck> playerDecks - список с колодами игрока
	public readonly int[] minimumForlevelUp - массив минимального кол-ва карт для апгрейда для каждого уровня 
	public PlayerCard cardByCardIdAndLevel(int _cardId, int _level) - находит карту из имеющихся по карт айди и уровню
	public void addCardToPlayer(int _cardId, int _level , int _count ) - добавляем карты игроку, если с таким id и уровнем уже существуют, то добавляем нужное кол-во
	public void upgradeCardToNextLvl(int localId, int currentLevel) - апгрейд карты
	public void addDeck(DeckType _deckType) - создать новую колоду
	public void SetActiveDeck(int index) - установить текущую колоду по индексу
	public void addCardToDeck( CardInfo cardInfo, int deckId) - добавить карту в колоду

//class PlayerCard
	public int localId - ID внутри списка карт игрока (те, которые открыл)
	public int cardId - ID в списке всех карт
	public int level - уровень карты
	public int count - количество карт данного уровня

//class PlayerDeck
	public int deckId - id колоды
	public DeckType deckType - тип колоды
	public List<int> cards - список карт в колоде
	public void addCard(int localId) - добавить карту в колоду

//////////////////////////////////////////////////////////////////////////////////////
//ShopController.cs
Контроллер, отвечающий за магазин // пока только номинальный... 
имеет поля и методы:
	private Meta meta - ссылка на мету
	private ShopView view - вьюха магазина
	private GameObject ShopSlotPrefab - префаб слота в магазине
	private TextAsset jsonShopAsset - //временный файл json для теста загрузки карт в магазине
	public override void Init(Meta meta) - Инит, запрос данных магазина, инит вьюхи, подписка на события
		//спавн слотов(далее надо перенести на момент получение ответа)
	void spawnShopSlotItems() - спавн всех слотов в магазине (вьюха)
	private void AddNewSlotItem(OnShopSlotAddNewEventArgs args) - добавить слот (вьюха)
	private void ClearShopSlots() - очистка магазина (вьюха)
	public void buyCard(OnTryingToBuyEventArgs args) - покупка карты


