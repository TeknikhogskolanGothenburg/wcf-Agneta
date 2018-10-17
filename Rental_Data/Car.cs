namespace Rental_Data
{ 
    public class Car
    {
        public string RegNumber { get; set; }  //ändrar till String, regnumber har bokstäver
        public string Brand { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public bool IsRented { get; set; }
    }
}