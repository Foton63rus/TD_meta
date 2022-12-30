using UnityEngine;
using UnityEngine.UIElements;

namespace TowerDefence
{
    [RequireComponent(typeof(UIDocument))]
    public class ShopView : MonoBehaviour
    {
        private UIDocument UIDoc;
        private VisualElement root;

        public void OnEnable()
        {
            UIDoc = GetComponent<UIDocument>();
            root = UIDoc.rootVisualElement;
        }

        public GroupBox AddRowForBanners()
        {
            GroupBox gb = new GroupBox();
            gb.AddToClassList("banner_row");
            root.Add( gb );
            
            return gb;
        }

        public GroupBox AddBanner( GroupBox row, int unique_id, Sprite background ,int price, string discount, int count )
        {
            GroupBox banner = new GroupBox();
            banner.AddToClassList("banner_item");
            banner.style.backgroundImage = new StyleBackground( background );
            row.Add(banner); 
            
            Label priceLabel = new Label($"prise: {price}");
            priceLabel.AddToClassList("banner_price");
            banner.Add( priceLabel );
            
            Label discountLabel = new Label($"Discount: {discount}");
            discountLabel.AddToClassList("banner_discount");
            banner.Add( discountLabel );
            
            Label countLabel = new Label($"Count: {count}");
            countLabel.AddToClassList("banner_num");
            banner.Add( countLabel );
            
            Button btn = new Button();
            btn.AddToClassList("banner_button_overall");
            banner.Add( btn );
            btn.clicked += () =>
            {
                MetaEvents.BannerClicked?.Invoke(unique_id);
            };
            
            return banner;
        }
    }
}