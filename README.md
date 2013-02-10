# From the desk of Matthew Kastor
On this glorious Sunday, February 10, 2013


## Intended Recipients
Public


## Summary
Redirect stderr and stdout to the console and a file and a windows form and a webservice and . . . your grandma? All in realtime.


## Contents
The grand adventures of a nerd with logorrhea


## Message 

Redirect stderr and stdout to the console and a file and a windows form and a webservice and . . . your grandma? All in realtime.

The folder named Redirect contains a Visual Studio 2012 Express for desktop VB project that will produce a library with all the necessary functions to redirect stdout and stderr to multiple locations. All you need to do is add a module using the functions and change the project properties application type to either a windows forms application or a console application. By overriding Redirector.StdoutHandler and Redirector.StderrHandler functions you'll easily be able to send the data streams wherever you see fit, in real time. There is no waiting for the spawned process to exit before the data comes through, which is ideal for long running processes.

Alternatively, you could create another project in the solution and add a reference to the Redirector project. Also add a dependency from your new project to the Redirector, and then Import the redirector into your new project. Doing it this way, you'll end up with a Redirector.dll along with your new project. You could then copy that dll into any new project in a different solution and reference it instead of bringing the source into the solution.

The folder named Gecko is a practical application of the Redirector class. While experimenting with XULRunner, the open source Gecko runtime products like Firefox and Thunderbird are built on, I needed access to the console output in real time. I am aware that there is a commandline switch `-console` that will spawn a new console along with the main window of the program. The thing is, you can't do anything with that console window and when you send a command to terminate XULRunner's process that console window closes. Sometimes I need to read the console, other times I need to log what's coming out of it and I'm not really into cut and paste. Besides, on linux and mac I hear the console output just shows up in the terminal that launched XULRunner, no fancy switches and dead windows necessary. This bothered me for a bit but never enough to do anything about it. That is, until I found a project here on the githubs called SlimerJs.

SlimerJs wants to do on Firefox/Conkeror/XULRunner/Gecko what PhantomJs does on Chrome/Safari/Webkit. I think that would be awesome but, there we were staring at this dumb `-console` again... To be honest, I was like "fuck it, they'll let us redirect to a file >somefile 2>&1 but not the console >CON 2>&1, it's probably not worth trying something more heavy duty". laurentJ was all like "aww man, this is not ideal" Which I took to mean "What a bunch of bullshit! Seriously?!?! What the hell did they do here?" . . . 

Strapping on my beer helmet and my nerd squit I got to tapping at the keys. I thought, let's see if I can bang this out in Jscript and compile it with jsc... I mean what's the point in doing something if there's all kinds of documentation about how to do it, that just means someone already did it but likes to talk a lot while you're reading through their gibberish and run on sentences thinking "For fucks sake man! Get to the point!" Well, the Jscript turned out to be a dead end. I kept trying to assign a handler to the stderr and stdout events but nothing I was doing would stop the compiler from choking and puking up errors. Somewhere between the beer and research I could give two shits about, I read something about Jscript not supporting delegates or some shit. Then I read some more and stumbled across something that said "Sure you can add event handlers, you just can't add events" and I was like "Oh SNAP!" Because I still hadn't found out how to get the compiler to stop telling me that it couldn't change my callback function into the proper type or something... I'm saying I was feelin a bit tipsy and, as everyone knows, Notepad++ is too powerful a tool to operate under the influence. I needed something with more cushion, something that would ease my inebriated fingers by finishing my sentences for me, something that would almost write entire applications for me... so I whipped out the Visual Studio Express End of the World Edition. Bang, there it was. . . And it's been awhile since I sat down and thought in VB. It's a beautiful language really. I mean, look at the end result, I've written a modular utility that does multiple asynchronous tasks to solve a problem that only bothers hardcore nerds (immensely). There are callbacks and events and handlers, classes and singletons and functions, static typing or I can pretend it's not, and the best part about it all is that I didn't have to tie knots with the code by writing the last thing to do first and spaghettifying my brain to keep the logic straight. Plus, the editor did all the heavy lifting for me. I tell ya, with that there intellisense and project generation about all I had left to do is actually write code!

So there I was in the visual fluffy cloud of code and decent documentation, hallucinating about the day I'd be done whipping up little tools for no reason when it hit me. I was finished. I'd written up my little utility and integrated it into my problem so it could stretch out and blow that shit up like a tentacle monster in a bad cartoon. Woot!

What I'm saying is, the Gecko folder illustrates a use case for the Redirector. I first extend the redirector and create GeckoLib. GeckoLib is generic enough to launch the Gecko runtime [FF,TB,XR,etc.] and redirect its stderr and stdout wherever I want. I happened to want it in the same console I use to launch the runtime, so I left the handlers alone. Now, the GeckoLib generates a dll so I wrapped it up with a console application and called it Console Redirector. It wrks gud. Then I got to my real issue, I was wearing a hard hat with empty beer cans in it. :( It's allright, I've got buns of steel, and I wasn't about to get up and go for more when I was almost finished, or I was too drunk, you decide.

Major disasters and imminent doom aside, I continued on valliantly. I took a hard look at what I had built and what it had to connect to and built the bridge I needed by extending GeckoLib into SlimerJsLib, then wrapped it all up in a nice crap taco! It was awesome, I was finally done and I might have retrained myself not to put semicolons at the end of every statement. :O


Environment.Exit(awesome)



## Signature
Matthew Christopher Kastor-Inare III
☭ Hial Atropa!! ☭


### Contact Info
*Phone*:
    (616) 439-0091

*Email*:
    matthewkastor@gmail.com

*Google Profile*:
    https://plus.google.com/100898583798552211130/posts

*Blogger*:
    http://matthewkastor.blogspot.com/

*Github*:
    https://github.com/matthewkastor?tab=repositories


### Disclaimer

This email and any files transmitted with it are confidential, privileged, and/or otherwise protected by work-product immunity or other legal rules. This email and any files transmitted with it are intended solely for the use of the individuals or entities to whom they are addressed. If you are not the intended recipient, you are notified that disclosing, copying, distributing or taking any action in reliance on the contents of this information is strictly prohibited. If you have received this email in error, please notify Mr. Kastor immediately. Mr. Kastor accepts no liability for the content of this email or for the consequences of any actions taken on the basis of the information provided.

*WARNING*: Computer viruses can be transmitted via email. The recipient should check this email and any attachments for the presence of viruses. Mr. Kastor accepts no liability for any damage caused by any virus transmitted by this email. Email transmission cannot be guaranteed to be secure or error-free, as information could be intercepted, corrupted, lost, destroyed, arrive late or incomplete, or contain viruses. Mr. Kastor, therefore, does not accept liability for any errors or omissions in the contents of this message which arise as a result of email transmission.
Copyright notice

This email and its content is copyright of Matthew Kastor - © Matthew Kastor 2013. All rights reserved.

Any transmission, redistribution, or reproduction of part or all of the contents in any form is strictly prohibited. You may not, except with Mr. Kastor's express written permission, distribute or commercially exploit the content. You may not transmit it or store it in any other form of electronic retrieval system other than the email system to which this message was originally delivered. Any written permission granting use of these contents must be notarized to be considered for validity.
