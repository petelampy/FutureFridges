# Future Fridges
This is the source code repository for the Future Fridges application that was developed for SOFT30121: Advanced Analysis & Design as part of the BSc Software Engineering Degree.

## Code Architecture
The source code is broken down into 3 layer: Web, Business and Data. Each of these have their own purpose outlined below.

### Web
This layer consists of the majority of the plain Razor Pages and HTML content (eg. .cshtml, .cshtml.cs). These will be the content visible to the end user and the actions being performed at the highest level. This will all be under the "Pages" Folder within the project.

### Business
This layer is the layer performing the actual calculations, verifications, retrieval, etc. It essentially performs the "heavy lifting" of the codebase. This layer will main consist of .cs files and will be kept under the "Business" folder (with subfolders for each category such as StockManagement).

### Data
This layer is the layer responsible for interfacing with the database, such as making the actual database connection and retrieving the data, as well as converting it to appropriate business classes. These will also mainly consist of .cs files and will be stored under the "Data" folder (with appropriate subfolders as mentioned above)

## Variable Naming
To ensure we were consistent throughout the work on this codebase we followed these variable naming conventions:

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

## Screenshots
### Initial User Login Page
![image](https://github.com/user-attachments/assets/89755100-130b-4f7c-8d29-04e4f809840b)

### Welcome Screen
![image](https://github.com/user-attachments/assets/b4c93568-5f4a-4262-bbb1-f26c110d3b24)

### Main Menu with all available options
![image](https://github.com/user-attachments/assets/116e306a-3f7f-45bc-925f-35d0fb7cc813)

### Taking stock from the fridge
![image](https://github.com/user-attachments/assets/c076ba4e-db4c-4c68-89b4-9aee7bbf082e)

### Adding stock to the fridge
![image](https://github.com/user-attachments/assets/3b70691e-94b8-4752-b378-a7415bded0c7)

### Managing Products
![image](https://github.com/user-attachments/assets/26e4fa72-cc09-4e36-886d-ea565718c3dc)

### Managing Users
![image](https://github.com/user-attachments/assets/ad1e5d4f-6c6f-4ec5-89fa-9d838fe2d50c)

### User Creation Email
![image](https://github.com/user-attachments/assets/e730cb8e-c8a7-4dca-bb08-a500b48a6669)

### User Permissions Management
![image](https://github.com/user-attachments/assets/492f95d0-51e9-4cf5-8363-4472badffd37)

### Creating an order
![image](https://github.com/user-attachments/assets/6dd8ecbe-09f0-48d6-a02b-07c0163c35a1)

### Stock Audit Log Page
![image](https://github.com/user-attachments/assets/d055ee23-5546-497a-afaf-01085b1dd845)

### Stock Notifications System
![image](https://github.com/user-attachments/assets/327a9eb2-c98a-4d54-9521-199420af19a7)
