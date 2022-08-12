namespace Entities
{
    /*
   Returns: Zero or more products (limited to 1000 products) that match the search.

      Results Object
          Found = "Y" (or "N" if there are no search results)
          SearchID – Treat this like a session ID
          HitCount = Total number of products found (limited to 1000).
          Result array (of SearchResult objects)

      SearchResult Object
          Description – Description of the product or itemcode
          Products = array of Product objects

      Product object
          Same as regular product object minus the Price object
   * */

    //public enum enumSearchResultStatus
    //{
    //    Found = 'Y',
    //    NotFound = 'N'
    //}


    public class Result
    {
        public string? Description { get; set; }
        public List<Product>? Products { get; set; }
    }

    public class Root
    {
        public string? HitCount { get; set; }
        public List<Result>? Results { get; set; }
        public string? SearchID { get; set; }
        public string? Found { get; set; }
    }
    public class Product
    {
        public string? Class0 { get; set; }
        public string? Barcode { get; set; }
        public string? ItemDescription { get; set; }
        public string? DeptID { get; set; }
        public string? SubClass { get; set; }
        public string? Class0ID { get; set; }
        public string? SubDeptID { get; set; }
        public string? Description { get; set; }
        public string? ItemCode { get; set; }
        public string? SubDept { get; set; }
        public string? ClassID { get; set; }
        public string? ImageURL { get; set; }
        public string? Dept { get; set; }
        public string? SubClassID { get; set; }
        public string? Class { get; set; }
        public string? ProductKey { get; set; }
    }



}