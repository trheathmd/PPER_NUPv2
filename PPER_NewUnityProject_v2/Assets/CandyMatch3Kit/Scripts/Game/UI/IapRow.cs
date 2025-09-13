// Copyright (C) 2017-2022 gamevanilla. All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement,
// a copy of which is available at http://unity3d.com/company/legal/as_terms.

using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

using GameVanilla.Core;
using GameVanilla.Game.Common;
using GameVanilla.Game.Popups;

namespace GameVanilla.Game.UI
{
    /// <summary>
    /// An in-app purchasable item in the shop popup.
    /// </summary>
    public class IapRow : MonoBehaviour
    {
        [HideInInspector]
        public BuyCoinsPopup buyCoinsPopup;

#pragma warning disable 649
        [SerializeField]
        private GameObject mostPopular;
        [SerializeField]
        private GameObject bestValue;
        [SerializeField]
        private GameObject discount;
        [SerializeField]
        private Text discountText;
        [SerializeField]
        private Text numCoinsText;
        [SerializeField]
        private Text priceText;
        [SerializeField]
        private Image coinsImage;
        [SerializeField]
        private List<Sprite> coinIcons;
#pragma warning restore 649

        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        private void Awake()
        {
            Assert.IsNotNull(mostPopular);
            Assert.IsNotNull(bestValue);
            Assert.IsNotNull(discount);
            Assert.IsNotNull(discountText);
            Assert.IsNotNull(numCoinsText);
            Assert.IsNotNull(priceText);
            Assert.IsNotNull(coinsImage);
        }

        /// <summary>
        /// Called when the purchase button is pressed.
        /// </summary>
        public void OnPurchaseButtonPressed()
        {
            // Commented out IAP logic since IAPManagerHandler is disabled/removed
            // IAPManagerHandler.instance.BuyConsumable(cachedItem.storeId);
            // buyCoinsPopup.OpenLoadingPopup();
            GetComponent<PlaySound>().Play("Button");
        }

        // Commented out IapItem object and Fill method
        /*
        private IapItem cachedItem;

        /// <summary>
        /// Fills this widget with the specified IAP item's information.
        /// </summary>
        /// <param name="item">The IAP item with which to fill this widget.</param>
        public void Fill(IapItem item , string price)
        {
            cachedItem = item;

            numCoinsText.text = item.numCoins.ToString("n0");
            if (item.discount > 0)
            {
                discountText.text = string.Format("{0}%", item.discount);
            }
            else
            {
                discount.SetActive(false);
            }

            if (item.mostPopular)
            {
                bestValue.SetActive(false);
            }
            else if (item.bestValue)
            {
                mostPopular.SetActive(false);
            }
            else
            {
                mostPopular.SetActive(false);
                bestValue.SetActive(false);
            }

            coinsImage.sprite = coinIcons[(int)item.coinIcon];
            coinsImage.SetNativeSize();

            //if (storeController != null)
            //{
            //    var product = storeController.products.WithID(item.storeId);
            //    if (product != null)
            //    {
                    priceText.text =  price;
                //}
            //}
          //  #else
 //   priceText.text = "$5,99";
}
*/
    }
}
