INSERT INTO tours."TouristEquipments"(
    "Id", "TouristId", "EquipmentIds")
VALUES (10, 1, ARRAY[1, 2, 3]);

INSERT INTO tours."TouristEquipments"(
    "Id", "TouristId", "EquipmentIds")
VALUES (-3, 10, ARRAY[1, 2, 3]);