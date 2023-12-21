i# OptimizedSpaceGame
optimizing a space shooter
///////////////////////////////////////
I made this whole game by myself, no code was taken from other projects or people. The github repository showed that it was a fork from another game by another person is shown because
I was working on it from my other github account after I created it innitially on my first account. I wrote the code, zipped and tried to send multiple times but binaries keep 
getting deleted if it doesnt work again, maybe we could discuss getting it over to you in a flashdrive where all files will defintly be saved.
///////////////////////////////////////





I set my benchmark at 5000 objects. After innitial test of my project I recieved frame rate average of ~5.5 with highest framerate of 6.5. 
I decided to use Jobs and burst compiler to optimize my project and reached a framerate average of 6.5-7 after using job + burst compiler.
This is a small difference of course but I belive it could be larger in a project where the objects have more than 1 job , which was calcu-
lating next position in my project. 


I decided to use Jobs system to utilize multiple CPU threads rather than only the main thread. However jobs can only do simple operations with non-
complex variables like ints, bools, vectors, etc. So I used jobs to calculate the next position of meteor's movement , return it and use it on main 
thread to actually move the meteor with the returned new position. Combining this with a burst compiler that translates from IL/. NET bytecode 
to highly optimized native code makes sure to use more previously untapped processing power. Jobs + burst ended up working somewhat but would be much more efficient on
a larger scale, if I were to make a more complex game with more algorithms that would be able to get more use out of jobs, they would be able to save even more memory 
relative to how much jobs do in this project.

I noticed that most of the memory usage in my project while testing many objects was physics calculations related , because without physics and other 
memory usage I could achieve 60 FPS at 5000 objects. However objects have to have triggers or collision for me to be able to shoot at them,
these triggers would overlap or colliders would collide causing physics calculations to occur and require a lot of memory usage I even tried 
to make these triggers ignore the layer that they were on so they would only overlap with player, however that changed nothing. 
So it was something I could not fix without switching to entities system or writting an alternative to unity 2d colliders from scratch.


I tried to fix my issues with physics taking a lot of processing power by trying various types of colliders, but all 2d colliders had similar results
so I used with 2dbox colliders that were slightly better than circle colliders from the tests that I did, but not noticably. I also tried using 3d 
colliders that lowered cpu usage even though I would expect it to increase cpu usage, but I wasnt able to implement shooting and hitting meteors while
using 3d colliders so I kept using 2d colliders.

I also did a few other minor sollutions some of which were positive and some of which were negative. For example I tried to implement object pooling into the game
However it lowered average fps down to 4 fps. Pooling works well for certain types of objects that are spawned, but because my memory usage issues came from existing objects
and not spawning in objects pooling was in fact was slower. Pooling works great for small bursts of spawned objects like waves in a tower defence game becuase reusing objects
is better than spawning new ones for the memory usage. But because I was using objects that were spawned and stayed there for a while the pooling system just wasted resources 
because I would end up spawning the same amount of objects as I would normally. Pooling is still great however , just not for the benchmark test and game I was making 
I also ended up setting some variables that would never change in game to static, it of course didnt do much because this change only applied to 2 objects in scene: player
and meteor spawner, however if each meteor had it's own speed and maybe another variable it would be more efficien than keeping those variables public and dynamic.

To use jobs and burst compiler I had to use unity DOTS- Data-Oriented Technology Stack a way to program in unity using a data oriented method rather than
object oriented method. Most of it comes throught the use of entities, however I had issues with implementing entities and stuck to only using jobs and burst conpiling. 
Jobs include scheduling, executing and manually collecting garbage left behind in the secondary threads, in a way that is more data oriented than 
implementing methods as jobs is Unity's internal c++ based system, allowing me to run my script alongside Unity's internal processing. I used jobs for calculations of asteroid
positions. Asteroids in my game travel towards 0,0 coordinates where player spawns. I tried doing those calculations in update first without any optimizations and achieved aproximately
5 fps on average. After that i kept running the change in position in update but calculated the next position in jobs as jobs can only do simple mathematical calculation 
with simple variables like floats and int. So I would run a job in update to calculate the asteroids next position then set the position of the asteroid for that position.
This would happen 5000 per frame, once for each asteroid which was still very memory consuming, but more effective than running calculations that could be done on other cores
on main thread.

Another way I used a data oriented method is through the use of a burst complier , a compiler that simplified human readable code and
variables to a simpler data files that are easier for CPUs to handle. I also did various experiments such as using Unity.Mathematics' various variable types 
such as int2, uint and Unity.mathematic Random  which are faster and simplier for to handle than vector2,int and Random.range respectively due to ease
of performing operations on these values in binary form. However I ended up scrapping those uses of these variables as they were used for some 
less efficient movement for meteors that would do more calculations , even if the variables were simpler and more data-oriented. 

I also tried to use ECS system to work more with Data oriented programming , however I had issues with rendering entities and reading an input for it so I could not
implement entities properly. But in the files there is a scene and some files using ecs. Specifically I made a ecs system that spawned in gameobjects that got turned into
entities. In which I created a custom baker for the spawner object that communicated with multiple other non-monobehaviour classes and structs, to spawn in object rather
than having each of those classes and structs as a seperate component, making it harder to implement but easier for cpu to read. If I were to continue using entities I would
have an entirely data-oriented method and a more memory efficient game.

I added some screenshots showing cpu usage of scripts before and after optimization as well as physics taking up a wast majority of cpu usage. 

Also I messed up github branches so the main branch is not functioning, check "final version" branch


Code Samples below:



///////// Meteor with a job to calculate their next position
public class Meteor : Damagable
{
		// using burst compiler for better memory usage
    [BurstCompile(CompileSynchronously = true)]
    private void Update()
    {
		// create a native array for a vector2 to save position for later
        NativeArray<Vector2> result = new NativeArray<Vector2>(1, Allocator.TempJob);
	// create new job
        DetermineNextPosition Job = new DetermineNextPosition
        {
	// variables used in job
            currentPosition = transform.position,
            targetPosition = Vector2.zero,
            nextPosition = result
        };
		// schedule job
        JobHandle jobHandle = Job.Schedule();
        jobHandle.Complete();

		// use results and clear used memory 
        transform.position = result[0];
         //transform.position = Vector2.Lerp(transform.position, Vector3.zero, 0.001f);
        result.Dispose();

    }
}

//////// Job that calculates next position
[BurstCompile(CompileSynchronously = true)]
public struct DetermineNextPosition : IJob
{
    public Vector2 currentPosition;
    public Vector2 targetPosition;


    public NativeArray<Vector2> nextPosition;

    public void Execute()
    {
        nextPosition[0] = Vector2.Lerp(currentPosition, targetPosition, 0.001f);
    }
}

