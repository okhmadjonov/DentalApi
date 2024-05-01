insert into "Students" ("Id","Name_en", 
"Name_ru","Name_uz","IsDeleted",  "CreatedAt", "UpdatedAt") 
values
(1,
'Alisher',
'Aleksey',
'John',
0,
'2023-11-23 16:13:56.461 UTC',
'2023-11-23 16:13:56.461 UTC');



INSERT INTO Migrations (MigrationName, CreatedAt) VALUES ('20240501120100_AddStudentValues', CURRENT_TIMESTAMP);