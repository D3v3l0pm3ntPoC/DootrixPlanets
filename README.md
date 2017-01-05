# DootrixPlanets

HOSTED LOCATION
================
Public REST API => http://www.planets.olan-george.com/

Has two endpoints, which are as follows => 
* http://planets.olan-george.com/api/Planets/ListAll
* http://planets.olan-george.com/api/Planets/GetByName

Public UI => http://www.planetsui.olan-george.com

However, this repository can also be pulled and the code base run locally. Everything should work fine.


OVERVIEW
=========
DATABASE => The datastore used is MongoDB and i have deployed a cloud version of the database. This is to ensure that when the code is pulled, there would be no need in setting up databases. The credentials for the cloud DB is in the configuration file, the contents of the collection can be inspected using any visual IDE for NoSQL (specifically MongoDB)

REST API => Encapsulates exposes endpoints for retrieving planet data from the datastore. Implicitly supports full CRUD functionalities. But for the deployed api, only read operations can be executed as there is no need for write operations (based on task spec).

However, using integration tests or even locally running the api, write operations can be performed.

UI => The UI only has a reference to the REST API's url, it doesn't know about the underlying implementation of the API or the datastore.

I have tried to demonstrate the principles of a distributed application (even without the benefit of time to actually do proper development). 

AREAS OF IMPROVEMENT
====================
Logs => Writing logs or better still raising analytic event messages to some persistent queue (that's subsequently processed by some processor that knows what to do with these analytic data).

Authentication => I had implemented plumbing work for authentication but abandoned it as i really didn't have the time to start getting clever.

Better Test Coverage => Even though i implemented TDD in driving the development, i probably could have added a bit more tests but i am confident the test coverage covers the important requirements for the task spec.


CONCLUSION
==========
I have tried to ensure that all dependencies are part of the repository. However, i apologise in advance if any library escapes my attention. I have literally had little or no time to work on this...and as a failsafe i have managed to get the versions of the api and ui published online. See above..

Thank you

