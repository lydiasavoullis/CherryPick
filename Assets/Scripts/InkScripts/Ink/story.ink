INCLUDE variables.ink
INCLUDE category_1.ink
INCLUDE tutorial.ink
EXTERNAL ChangeSprite(name,sprite)
EXTERNAL RemoveCharacter(name)
EXTERNAL AddCharacter(name,sprite)
// ->tutorial_pt1
->day_0
==tutorial_pt1==
~tutorialpt1 = "incomplete"
~tutorialpt2 = "incomplete"
~tutorialpt3 = "incomplete"
~tutorialCounter = 0
~currentSpeaker = "Alex"
¬
Hi
You must be the new recruit
I'll give you a run down of how things work around here
I'm sure it will be super easy for you
You can see you are equiped with this virtual interface
The lower box is your inventory
Here you can see what resources you have
Above that are your tasks that clients will give you
Now at here at Cherrypick we have BioAcceleration technology
Here's the fun part
To BREED two flowers, LEFT click on one and RIGHT click on another
The resulting SEEDS will appear on the counter
Have a go yourself then call me when you're done
->END
==tutorial_pt2==
~tutorialpt1="complete"
Great you just bred two flowers together
The resulting seeds will have a mixture of the genetics of the pair of flowers you just bred
Unfortunately the parents are destroyed in the process
Now I will show you how to plant a seed
Click on a seed and drag it to a pot
Then pick up a watering can and water a pot
->END
==tutorial_pt3==
~tutorialpt2="complete"
OK perfect
Tomorrow that will grow into a flower
You can thank our BioAcceleration technology
Don't worry, we're almost there 
Finally I'll show you how to complete an order
~task = "Alex,1,1,colour:pink,height:tall"
I've just given you a task, can you see it?
~gift = "1,colour:Rr,height:TT"
I've also put a corresponding flower in your inventory
drag that flower to the task and click 'sell' to complete the order
->END
==tutorial_pt4==
~tutorialpt3="complete"
Awesome
Keep in mind that the flower must match the phenotype described in the order
And you must place the correct quantity of flowers
Also tasks will have a deadline, they must be completed in a certain number of days
We can lose reputation from failed tasks so please be punctual
Great, you're ready to start the day
Ok byee
~characters-=alex
->day_0


==day_0==
->tutorial_vera
 ~characters+=yulia
 ~currentSpeaker = "you"
 ¬
//Uh hi
~currentSpeaker = "Yulia"
// Hey 
//~gift = "5,colour:Rr,height:tt,petals:five,clusters:CC,split:ss"
I would like two tall plants with red flowers
~task = "Yulia,1,3,colour:red,height:tall, petals:6"
~currentSpeaker = "you"
Ok, thanks for putting in that order
~characters-=yulia
// ~currentSpeaker = "you"
// Ok
// they're gone
// great
~end_of_day = "true"
~currentSpeaker = "you"
// It's the end of the day
// I can buy something new
~shop_state = "open"
//And maybe work on a few orders before I go to bed
-> END


==day_1==
~end_of_day = "false"
~currentSpeaker = "you"
Wow
It's a new day
time for some new work
~currentSpeaker = "Charlie"
~characters+=charlie
¬
Hey
Over here
I want a short white flower please
~task = "Charlie,1,3,colour:white,height:short"
~characters-=charlie
~end_of_day = "true"
~currentSpeaker = "you"
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END
==day_2==
~shop_state = "closed"
~end_of_day = "false"
Wow
It's another day
time for some new work
~currentSpeaker = "Yulia"
 ~characters+=yulia
¬
Hey 
Sorry to bother you
I would like a tall plant with pink flowers
~task = "Yulia,1,3,colour:pink,height:tall"
~currentSpeaker = "you"
Ok, thanks for putting in that order
~characters-=yulia
~end_of_day = "true"
Oh look
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END
==day_3==
~end_of_day = "false"
Wow
It's another day
time for some new work
~currentSpeaker = "Charlie"
~characters+=charlie
¬
Hey
Over here
I want a short pink flower please
~task = "Charlie,1,2,colour:pink,height:short"
~characters-=charlie
~end_of_day = "true"
~currentSpeaker = "you"
Oh look
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END
==day_4==
~end_of_day = "false"
Wow
It's another day
time for some new work
 ~characters+=yulia
¬
Hey 
Sorry to bother you
I would like a tall plant with pink flowers
~task = "Yulia,1,3,colour:pink,height:tall"
~currentSpeaker = "you"
Ok, thanks for putting in that order
~characters-=yulia
~end_of_day = "true"
Oh look
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END
==day_5==
~end_of_day = "false"
Wow
It's another day
time for some new work
~currentSpeaker = "Charlie"
~characters+=charlie
¬
Hey
Over here
I want a short pink flower please
~task = "Charlie,1,2,colour:pink,height:short"
~characters-=charlie
~end_of_day = "true"
~currentSpeaker = "you"
Oh look
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END