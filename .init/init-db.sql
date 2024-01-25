CREATE TABLE "Contents" (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(255) NOT NULL
);

INSERT INTO "Contents" ("Name")
VALUES
    ('Symptoms of a Heart Attack'),
    ('Coping with Headaches'),
    ('Pregnancy and Your Diet');