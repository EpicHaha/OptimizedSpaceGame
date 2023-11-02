# OptimizedSpaceGame
optimizing a space shooter

I set my benchmark at 5000 objects. After innitial test of my project I recieved frame rate average of ~5.5 with highest framerate of 6.5. 
I decided to use Jobs and burst compiler to optimize my project and reached a framerate average of 6.5-7 after using job + burst compiler.
This is a small difference of course but I belive it could be larger in a project where the objects have more than 1 job , which was calcu-
lating next position in my project. 

I noticed that most of the mermory usage in my project while testing many objects was physics calculations , because without physics and other 
memory usage I could achieve 60 FPS at 5000 objects. However objects have to have triggers or collision for me to be able to shoot at them 
so it was something I could not fix without switching to entities system or writting an alternative to unity 2d colliders from scratch.

I decided to use Jobs system to utilize multiple CPU threads rather than only the main thread. However jobs can only do simple operations with non-
complex variables like ints, bools, vectors, etc. So I used jobs to calculate the next position of meteor's movement , return it and use it on main 
thread to actually move the meteor with the returned new position. Combining this with a burst compiler that translates from IL/. NET bytecode 
to highly optimized native code makes sure to use more previously untapped processing power. 

I tried to fix my isseus with physics taking a lot of processing pwer by trying various types of colliders, but all 2d colliders had similar results
so I used with 2dbox colliders that were slightly better than circle colliders from the tests that I did, but not noticably. I also tried using 3d 
colliders that lowered cpu usage even though I would expect it to increase cpu usage, but I wasnt able to implement shooting and hitting meteors while
using 3d colliders so I kept using 2d colliders.

To use jobs and burst compiler I had to use unity DOTS- Data-Oriented Technology Stack a way to program in unity using a data oriented method rather than
object oriented. Most of it comes throught the use of entities, however I had issues with implementing entities and stuck to only using jobs and burst. 
However I still used a data oriented method by using a burst compiler that simplified human readable code and variables to a simpler data files that are easier
for cpus to habdle.


I added some screnshots showing cpu usage of scripts before and after optimization as well as physics taking up a wast majority of cpu usage. 

Also I messed up github branches so the main branch is not functioning, check "final version" branch