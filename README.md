# Future Fridges
This is the source code repository for the Future Fridges application being developed for SOFT30121: Advanced Analysis & Design.

## Code Architecture
The source code is broken down into 3 layer: Web, Business and Data. Each of these have their own purpose outlined below.

### Web
This layer consists of the majority of the plain Razor Pages and HTML content (eg. .cshtml, .cshtml.cs). These will be the content visible to the end user and the actions being performed at the highest level. This will all be under the "Pages" Folder within the project.

### Business
This layer is the layer performing the actual calculations, verifications, retrieval, etc. It essentially performs the "heavy lifting" of the codebase. This layer will main consist of .cs files and will be kept under the "Business" folder (with subfolders for each category such as StockManagement).

### Data
This layer is the layer responsible for interfacing with the database, such as making the actual database connection and retrieving the data, as well as converting it to appropriate business classes. These will also mainly consist of .cs files and will be stored under the "Data" folder (with appropriate subfolders as mentioned above)

## Code flow
The code will flow between these layers when performing actions as outlined with the example of retrieving an item of stock below.

**Web Layer** - User enters a stock item ID and clicks the "search" button. This button click is handled in the cshtml.cs for the page, within an appropriate method. This method will then create a new instance of the Business class "StockItemController". It will then call the method "GetStockItem" from this Business class.

![image](https://user-images.githubusercontent.com/34271471/194389539-233a90bd-40cc-4e9f-be6e-2e3123fad3f7.png)


**Business Layer** - The business class method for GetStockItem will be run, and this will make a call to "StockItemRepository" GetStockItem method in the Data layer.

![image](https://user-images.githubusercontent.com/34271471/194389934-761abaee-6257-42c0-8ef6-81208abef6a1.png)

![image](https://user-images.githubusercontent.com/34271471/194389670-8b788413-dc69-45cb-a2d1-471fb7b93a7d.png)


**Data Layer** - The GetStockItem method in the data class will be run, and will contact the database and fetch the appropriate record, then convert it into the appropriate Model to be used in the codebase.

![image](https://user-images.githubusercontent.com/34271471/194389824-0bbad505-b462-4170-82ca-55f0ab977b8a.png)

![image](https://user-images.githubusercontent.com/34271471/194389776-5ed4c9d8-b0c9-44f8-8b6f-60dc49d83f41.png)

The model from the Data layer is then returned to the Business method, which in turn returns it to the Web layer to display to the user.

## Models/Enums
We should use Models for every database item that is going to be represented within the codebase, this allows us to convert the raw data from the database into an easy to use format within the code that can be passed between pages. For values that have a few specific options that will not change, we should use Enums and then store the number value of the enum in the database rather than the word to save memory and reduce complexity.

### Example Model
![image](https://user-images.githubusercontent.com/34271471/194391956-06314b01-7b5e-4e06-8639-678e4787cbc5.png)

### Example Enum
![image](https://user-images.githubusercontent.com/34271471/194392033-71b6efd3-d02e-44bd-bb2e-d8ea242abea8.png)


## Variable Naming
To ensure we are consistent throughout the work on this codebase we will follow these variable naming conventions:

### Local Variables
Local variables (variables within a particular method, etc) will be TitleCase and begin with a single underscore

![image](https://user-images.githubusercontent.com/34271471/194390625-be389740-744e-4c48-ad4c-266b6a99057f.png)


### Class Variables
Class variables (variables defined at the top of a class) will be TitleCase and begin with two underscores

![image](https://user-images.githubusercontent.com/34271471/194390794-0b94c888-4b16-4b10-9283-6541c7a57c3e.png)

### Function/Method Parameters
Parameters for methods/functions will be camelCase

![image](https://user-images.githubusercontent.com/34271471/194391016-f402950a-fb53-4161-9da5-e9dc1082e1fb.png)

### Constants
Constants will be capitalised, with an underscore between words

![image](https://user-images.githubusercontent.com/34271471/194391195-09f18a1d-5510-4bf0-ad0f-c2e64cf3cc02.png)


