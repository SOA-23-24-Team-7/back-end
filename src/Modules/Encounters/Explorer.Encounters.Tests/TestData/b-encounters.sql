INSERT INTO encounters."Encounters"(
	"Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances", "Picture")
	VALUES (-1, 'prvi', 'opis prvog', 45.45, 45.45, 1500, 5, 0, 0, '[]', 'https://static.vecteezy.com/system/resources/previews/009/273/280/non_2x/concept-of-loneliness-and-disappointment-in-love-sad-man-sitting-element-of-the-picture-is-decorated-by-nasa-free-photo.jpg');
INSERT INTO encounters."Encounters"(
	"Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances", "Picture")
	VALUES (-2, 'drugi', 'opis drugog', 45.45, 45.45, 1500, 5, 0, 0, '[{{"Status": 0,"UserId": -21,"CompletionTime": null}}]', 'https://static.vecteezy.com/system/resources/previews/009/273/280/non_2x/concept-of-loneliness-and-disappointment-in-love-sad-man-sitting-element-of-the-picture-is-decorated-by-nasa-free-photo.jpg');
INSERT INTO encounters."Encounters"(
	"Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances", "Picture")
	VALUES (-3, 'treci', 'opis treceg', 45.45, 45.45, 1500, 5, 0, 0, '[]', 'https://static.vecteezy.com/system/resources/previews/009/273/280/non_2x/concept-of-loneliness-and-disappointment-in-love-sad-man-sitting-element-of-the-picture-is-decorated-by-nasa-free-photo.jpg');

INSERT INTO encounters."SocialEncounters" ("Id", "PeopleNumber") VALUES (-2, 1);

INSERT INTO encounters."TouristProgress"(
	"Id", "UserId", "Xp", "Level")
	VALUES (-1, -21, 0, 1);