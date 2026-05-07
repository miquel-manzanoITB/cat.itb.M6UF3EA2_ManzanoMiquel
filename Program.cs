using System.Diagnostics;
using cat.itb.M6UF3EA2_ManzanoMiquel.cruds;

public class Program
{
    public static void Main()
    {
        const string MainMenuMsg = "Selecciona una opcio:\n" +
                                   "Exercici 1. Importació de col·leccions\n" +
                                   "Exercici 2. Consultes\n" +
                                   "Exercici 3. Actualitzar documents\n" +
                                   "Exercici 4. Eliminar documents\n" +
                                   "Exercici 5. Eliminar una collection\n" +
                                   "0. Exit.";
        const string ExTwoMenuMsg = "Selecciona una opcio del exercici 2:\n" +
                                   "a) Crea un mètode que compti i mostri tota la població de tots els països d’Europa de la col·lecció countries.\n" +
                                   "b) Crea un mètode que mostri la capital, la població i el contingut del camp \"latlng\" de Madagascar de la col·lecció\r\ncountries.\n" +
                                   "c) Crea un mètode que mostri el títol, el número de pàgines i les categories de tots els llibres i els ordenes per número\r\nde pàgines de més pàgines a menys pàgines.\n" +
                                   "d) Crea un mètode que li passis el zipcode com a paràmetre i et mostri només el nom i el tipus de cuina dels\r\nRestaurants d'aquest codi postal.\n" +
                                   "e) Crea un mètode que mostri totes les dades dels restaurants on borough = Bronx i cuisine = Chinese.\n" +
                                   "f) Crea un mètode que mostri els camps \"title\", \"pageCount\" i els \"autors\" dels llibres que tinguin menys de 130\r\npàgines.\n" +
                                   "g) Crea un mètode que mostri els noms dels amics de la persona Caroline Webster a la col·lecció \"people\".\n" +
                                   "0. Exit.";
        const string ExThreeMenuMsg = "Selecciona una opcio del exercici 3:\n" +
                                   "a) Crea un mètode per la col·lecció Restaurants que actualitzi el zipcode\" del carrer \"Driggs Avenue\", el nou\r\n\"zipcode\" serà \"10443\". El mètode ha de mostrar primer el document amb el zicopde antic, fer l’actualització i\r\ndesprés mostrar el zipcode actualitzat.\n" +
                                   "b) Crea un mètode que afegeixi el camp \"stockminim\" a tots els productes amb preu superior a 2000. El valor del\r\nnou camp ha de ser 20. El mètode ha de mostrar el número de documents actualitzats i també tots els camps dels\r\ndocuments actualitzats.\n" +
                                   "c) Crea un mètode per afegir un altre autor anomenat \"Sam Watters\", al llibre \"Code Generation in Action\". El\r\nmètode ha de mostrar primer el document amb els autors antics, fer l’actualització i després mostrar els autors nous.\n" +
                                   "d) Crea un mètode per afegir un nou camp anomenat gama a tots els productes. El valor del nou camp dependrà del\r\npreu del producte. Si el producte val entre 1 i 500 el valor de nou camp serà \"baixa\". Si el preu és entre 501 i 2000 el\r\nvalor de nou camp serà \"mitja\" i si el preu és més gran de 2000 el valor del nou camp serà \"extra\".\n" +
                                   "e) Crea un mètode per modificar les categories del producte amb nom \"MacBook Pro\", ja no té la categoria\r\n\"notebook\" la nova categoria és \"ipad\" Al mètode es passa com a paràmetres el nom del producte, la categoria antiga\r\ni la nova categoria. No saps en quina posició està la categoria. El mètode ha de mostrar el document abans i després\r\nde l’actualització.\n" +
                                   "f) Crea un mètode per actualitzar els productes que valen entre 800 i 1000. Posarem el Stock a 60. El mètode ha de\r\nmostrar el número de documents actualitzats i també tots els camps dels documents actualitzats.\n" +
                                   "g) Crea un mètode per afegir un nou codi a camp callingCodes del país Iceland a la col·lecció countries. El codi a\r\nafegir és el 356. El mètode ha de mostrar el document abans i després de l’actualització.\n" +
                                   "0. Exit.";
        const string ExFourMenuMsg = "Selecciona una opcio del exercici 4:\n" +
                                   "a) Crea un mètode que elimini tots els restaurants del barri (borough) de Manhattan. Mostra el el numero de\r\ndocuments eliminats.\n" +
                                   "b) Crea un mètode que elimini la primera categoria del producte anomenat \"iPhone 7\"\n" +
                                   "c) Crea un mètode per eliminar els llibres que el número de pàgines sigui entre 0 i 100. Mostra el el numero de\r\ndocuments eliminats.\n" +
                                   "d) Crea un mètode que elimini el producte anomenat \"Apple TV\".\n" +
                                   "e) Crea un mètode per eliminar l'última categoria del llibre amb ISBN igual a 1933988177.\n" +
                                   "f) Crea un mètode per eliminar tots els productes que tinguin la categoria de \"phone\".\n" +
                                   "g) Crea un mètode per eliminar el camp \"tags\" de tots els professors \"teacher\" de la col·lecció \"people\".\n" +
                                   "h) Crea un mètode per eliminar tots els països on es parli Español de la col·lecció countries.\n" +
                                   "0. Exit.";

        //CRUDs
        ProductCRUD productCRUD = new ProductCRUD();
        BookCRUD bookCRUD = new BookCRUD();
        CountryCRUD countryCRUD = new CountryCRUD();
        GradeCRUD gradeCRUD = new GradeCRUD();
        PersonCRUD personCRUD = new PersonCRUD();
        RestaurantCRUD restaurantCRUD = new RestaurantCRUD();
        StudentCRUD studentCRUD = new StudentCRUD();

        int userOption;
        string userSubOption;
        bool validOption;
        bool exit = false;
        bool exitSubMenu = false;

        do
        {
            Console.Clear();
            Console.WriteLine(MainMenuMsg);
            validOption = int.TryParse(Console.ReadLine(), out userOption);
            if (validOption)
            {
                switch (userOption)
                {
                    case 1:
                        Console.WriteLine("Loading all collections...");
                        bookCRUD.LoadBooksCollection();
                        countryCRUD.LoadCountriesCollection();
                        gradeCRUD.LoadGradesCollection();
                        personCRUD.LoadPeopleCollection();
                        productCRUD.LoadProductsCollection();
                        restaurantCRUD.LoadRestaurantsCollection();
                        studentCRUD.LoadStudentsCollection();
                        Console.WriteLine("---------- Finish ----------");
                        break;
                    case 2:
                        do
                        {
                            Console.WriteLine(ExTwoMenuMsg);
                            userSubOption = Console.ReadLine();

                            switch (userSubOption)
                            {
                                case "a":
                                    countryCRUD.GetTotalPopulationEurope();
                                    break;
                                case "b":
                                    countryCRUD.GetCountryDetails("Madagascar");
                                    break;
                                case "c":
                                    bookCRUD.GetBooksTitlePagesCategoriesOrderedByPages();
                                    break;
                                case "d":
                                    restaurantCRUD.GetRestaurantsByZipcode("10462");
                                    break;
                                case "e":
                                    restaurantCRUD.GetBronxChineseRestaurants();
                                    break;
                                case "f":
                                    bookCRUD.GetBooksUnder130Pages();
                                    break;
                                case "g":
                                    personCRUD.GetFriendsOfCarolineWebster();
                                    break;
                                case "0":
                                    exitSubMenu = true;
                                    break;
                                default:
                                    Console.WriteLine("Input no valid, a de ser una lletra en minuscula de les disponibles");
                                    break;
                            }
                        } while (!exitSubMenu);
                        break;
                    case 3:
                        do
                        {
                            Console.WriteLine(ExThreeMenuMsg);
                            userSubOption = Console.ReadLine();

                            switch (userSubOption)
                            {
                                case "a":
                                    restaurantCRUD.UpdateZipcodeOfDriggsAvenue();
                                    break;
                                case "b":
                                    productCRUD.AddStockMinimToExpensiveProducts();
                                    break;
                                case "c":
                                    bookCRUD.AddAuthorToCodeGenerationBook();
                                    break;
                                case "d":
                                    productCRUD.AddGamaFieldToAllProducts();
                                    break;
                                case "e":
                                    productCRUD.UpdateProductCategory("MacBook Pro", "notebook", "ipad");
                                    break;
                                case "f":
                                    productCRUD.UpdateStockForProductsBetween800And1000();
                                    break;
                                case "g":
                                    countryCRUD.AddCallingCodeToIceland();
                                    break;
                                case "0":
                                    exitSubMenu = true;
                                    break;
                                default:
                                    Console.WriteLine("Input no valid, a de ser una lletra en minuscula de les disponibles");
                                    break;
                            }
                        } while (!exitSubMenu);
                        break;
                    case 4:
                        do
                        {
                            Console.WriteLine(ExFourMenuMsg);
                            userSubOption = Console.ReadLine();

                            switch (userSubOption)
                            {
                                case "a":
                                    restaurantCRUD.DeleteManhattanRestaurants();
                                    break;
                                case "b":
                                    productCRUD.DeleteFirstCategoryFromIphone7();
                                    break;
                                case "c":
                                    bookCRUD.DeleteBooksUnder100Pages();
                                    break;
                                case "d":
                                    productCRUD.DeleteAppleTV();
                                    break;
                                case "e":
                                    bookCRUD.DeleteLastCategoryFromBookByISBN();
                                    break;
                                case "f":
                                    productCRUD.DeleteProductsWithPhoneCategory();
                                    break;
                                case "g":
                                    personCRUD.DeleteTagsFromTeachers();
                                    break;
                                case "h":
                                    countryCRUD.DeleteSpanishSpeakingCountries();
                                    break;
                                case "0":
                                    exitSubMenu = true;
                                    break;
                                default:
                                    Console.WriteLine("Input no valid, a de ser una lletra en minuscula de les disponibles");
                                    break;
                            }
                        } while (!exitSubMenu);
                        break;
                    case 5:
                        
                        break;
                    case 0:
                        exit = true;
                        break;
                }
            }
            else
            {
                Console.WriteLine("Not valid option");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        } while (!exit);
    }
}