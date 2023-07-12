# File-system, Faculty of Electrical Engineering, 2020

Application specification

This project involves the implementation of a simple Shell application, which serves as an interface for user interaction with the operating system. The application is console-based, allowing users to input commands and receive results in alphanumeric format.

The main functionalities supported by this Shell application are:

    Login: Users enter their username and password, and the application checks if they exist in a pre-created user account database. Upon successful login, the current directory for the user is determined.

    Displaying the current directory (where): It shows the path to the current directory where the logged-in user is located.

    Changing directories (go path): It enables users to change the current directory to a desired path.

    Creating directories/files (create [-d] path): It creates a new directory or file at the specified path. If the -d parameter is provided, a directory is created; otherwise, a file is created. If only the name is provided, a new directory/file is created in the current directory.

    Listing directory contents (list [path]): It displays the contents of the directory at the specified path or the current directory if no path is provided. The entire tree of directories and files rooted at the specified (or current) directory is formatted and displayed.

    Printing file contents (print file): It prints the contents of a textual file to the console. In case a non-textual file is specified, an appropriate message is displayed.

    Searching for text in a file (find "text" file): It searches the content of a file for the specified text. If the text is found, the command returns the line number where the text is found (first occurrence). The text is always single-line.

    Searching for a file in a directory tree (findDat file path): It searches the directory tree rooted at the specified path for a file with the same name as the first argument of the command.

The project implements validation of user input and proper display of relevant messages to the user in case of invalid command inputs.
