LIST characters=alex,beatrice,charlie
VAR currentSpeaker = "Alex"

LIST Alex = (alex_normal)
LIST Beatrice = (beatrice_normal)
LIST Charlie = (charlie_normal)

VAR tutorialpt1 = "complete"
VAR tutorialpt2 = "complete"
VAR tutorialpt3 = "complete"
VAR tutorialCounter = 2
~characters+=alex
~currentSpeaker = ""
VAR task = ""
VAR end_of_day = "false"
VAR gift = ""
//->tutorial_pt1
->day_0
==tutorial_pt1==
~tutorialpt1 = "incomplete"
~tutorialpt2 = "incomplete"
~tutorialpt3 = "incomplete"
~tutorialCounter = 0

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
->day_0
==day_0==
//  ~characters+=beatrice
//  ~currentSpeaker = "you"
//  ¬
// Uh hi, what can I help you with?
// ~currentSpeaker = "Beatrice"
// Hey 
// Sorry to bother you
// I'm here to collect whatever you have
// ~task = "Beatrice,2,3,colour:red,height:tall"
// ~currentSpeaker = "you"
// Ok, thanks for putting in that order
// ~currentSpeaker = "Alex"
// Hi, what's your name?
// ~currentSpeaker = "Beatrice"
// I'm Beatrice, you?
// ~currentSpeaker = "Alex"
// Alex
// ~task = "Charlie,1,3,colour:white,height:short"
// ~characters+=charlie
// ~currentSpeaker = "Charlie"
// ¬
// Boo
// Hey
// Over here
// ~characters-=alex
// ~characters-=beatrice
// ~characters-=charlie
~currentSpeaker = "you"
Ok
they're gone
great
~end_of_day = "true"
I can close up the shop and go to bed now Maybe I'll work on a few orders before it gets dark
¬
-> END
==day_1==
~end_of_day = "false"
Wow
It's a new day
time for some new work
I guess
~end_of_day = "true"
Oh look
It's the end of the day
->END
==day_2==
~end_of_day = "false"
Wow
It's another day
time for some new work
I guess
~end_of_day = "true"
Oh look
It's the end of the day
->END
==day_3==
~end_of_day = "false"
Wow
It's another day
time for some new work
I guess
~end_of_day = "true"
Oh look
It's the end of the day
->END
==day_4==
~end_of_day = "false"
Wow
It's another day
time for some new work
I guess
~end_of_day = "true"
Oh look
It's the end of the day
->END
==day_5==
~end_of_day = "false"
Wow
It's another day
time for some new work
I guess
~end_of_day = "true"
Oh look
It's the end of the day
->END