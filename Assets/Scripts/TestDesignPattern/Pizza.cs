using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ThangVN
{
    public interface Pizza
    {
        float GetPrice();
    }

    public class HamAdnMushroomPizza : Pizza
    {
        private float price = 5.5f;
        public float GetPrice() { return price; }
    }

    public class DeluxePizza : Pizza
    {
        private float price = 10.7f;
        public float GetPrice() { return price; }
    }

    public class SeafoodPizza : Pizza
    {
        private float price = 11.5f;
        public float GetPrice() { return price; }
    }

    public class PizzeFactory
    {
        public enum PizzaType
        {
            HamMushroom, Deluxe, Seafood
        }

      
    }
}
