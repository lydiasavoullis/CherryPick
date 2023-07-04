==tutorial_vera==
 ¬
~characters+=vera
~currentSpeaker = "Vera"
Hi, my name's Vera. I'm your assistant
 ~currentSpeaker = "you"
 Nice to meet you Vera, I've heard about you
{ChangeSprite("Vera", "vera_shocked")}
~currentSpeaker = "Vera"
It wasn't from the news, was it?
~currentSpeaker = "you"
Uh, no. It was from Emily, the previous owner
{ChangeSprite("Vera", "vera_smile")}
~currentSpeaker = "Vera"
Ah OK, hehe she was so nice
 ~currentSpeaker = "you"
Have you been on the news recently?
{ChangeSprite("Vera", "vera_normal")}
~currentSpeaker = "Vera"
No
~currentSpeaker = "you"
Are you sure?
{ChangeSprite("Vera", "vera_smile")}
~currentSpeaker = "Vera"
Nope. Not at all. Have a nice day. Bye.
~currentSpeaker = "you"
...
~characters-=vera
->yulia_repeatable_1
==tutorial_pt1==
~tutorialpt1 = "incomplete"
~tutorialpt2 = "incomplete"
~tutorialpt3 = "incomplete"
~tutorialpt4 = "incomplete"
~tutorialCounter = 0
 ~characters+=vera
~currentSpeaker = "Vera"
 ~save_button = "inactive"
 ~music = "future lofi"
¬
Hi
You must be the new recruit
I'll give you a run down of how things work around here
I'm sure it will be super easy for you
You can see you are equiped with this virtual interface
The side box is your inventory
Here you can see what resources you have
From the menu you can select the clipboard 
These are the tasks that clients will give you
Now at here at Cherrypick we have BioAcceleration technology
Here's the fun part
To BREED two flowers, LEFT click on one and RIGHT click on another
The resulting SEEDS will appear on the counter
~show_notification = "greenhouse"
Have a go yourself then call me when you're done
->END
==tutorial_pt2==
~tutorialpt1="complete"
~remove_notification = "greenhouse"
Great you just bred two flowers together
The resulting seeds will have a mixture of the genetics of the pair of flowers you just bred
Unfortunately the parents are destroyed in the process
Now I will show you how to plant a seed
Click on a seed and drag it to a pot
~show_notification = "greenhouse"
Then pick up a watering can and water a pot
->END
==tutorial_pt3==
~tutorialpt2="complete"
~remove_notification = "greenhouse"
OK perfect
Tomorrow that will grow into a flower
You can thank our BioAcceleration technology
Don't worry, we're almost there 
Finally I'll show you how to complete an order
~task = "Vera,1,1,colour:pink,height:tall"
I've just given you a task, can you see it?
~gift = "1,colourR:Rr,height:TT"
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
~music = ""
¬
~characters-=vera
~save_button = "active"
->day_0


-> END