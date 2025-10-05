System Monitor Library contains all the decoupled interfaces and OS specfic implementations for each available OS.
Current system I am using is windows. Therefore windows functionality has been tested. Used chatgpt and code assist for getting the implementation.
Linux system is just added for the sake of adding an implementation. I haven't tested this at any point.

The thought process was to create a seperate library so that we can integrate it with different sytems such as WPF, Winforms etc.
All the required metrices are abstracted away as interfaces and a factory was created so that platform specific implementation will only be generated when we try to get the matrices.

I did struggle a lot with Hosting and running the whole thing as a service. I could'nt complete that part on my own and I have resorted to code generation.
The library is the best I can do at my level and hosting process was a blatant rip off from chat gpt since I was not aware about it.
I really appreciate the opportunity and I am submitting the assignment. I see it is mentioned to provide more details on implementation but current time schedule won't permit me to go into more detail.

Thanks.
