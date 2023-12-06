INSERT INTO encounters."Encounters"(
	"Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances")
	VALUES (-1, 'prvi', 'opis prvog', 45.45, 45.45, 1500, 5, 0, 0, '[]');
INSERT INTO encounters."Encounters"(
	"Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances")
	VALUES (-2, 'drugi', 'opis drugog', 45.45, 45.45, 1500, 5, 0, 0, '[{{"Status": 0,"UserId": -21,"CompletionTime": null}}]');
INSERT INTO encounters."Encounters"(
	"Id", "Title", "Description", "Longitude", "Latitude", "Radius", "XpReward", "Status", "Type", "Instances")
	VALUES (-3, 'treci', 'opis treceg', 45.45, 45.45, 1500, 5, 0, 0, '[]');

INSERT INTO encounters."SocialEncounters" ("Id", "PeopleNumber") VALUES (-2, 1);

INSERT INTO encounters."TouristProgress"(
	"Id", "UserId", "Xp", "Level")
	VALUES (-1, -21, 0, 1);