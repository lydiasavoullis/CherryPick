LIST characters=alex,yulia,charlie,vera, mei
VAR currentSpeaker = ""
VAR shop_state = "closed"
LIST Alex = (alex_normal)
LIST Yulia = (yulia_normal), yulia_sleeping
LIST Charlie = (charlie_normal)
LIST Vera = (vera_normal), vera_shocked, vera_smile, vera_thinking
LIST Mei = (mei_normal), mei_shocked, mei_angry, mei_annoyed
VAR day = 0
VAR task = ""
VAR end_of_day = "false"
VAR gift = ""
VAR tutorialpt1 = "complete"
VAR tutorialpt2 = "complete"
VAR tutorialpt3 = "complete"
VAR tutorialCounter = 2
VAR show_notification = ""
VAR remove_notification = ""
VAR save_button = "active"