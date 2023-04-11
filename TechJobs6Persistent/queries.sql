-- Capture your answer here for each "Test It With SQL" section of this assignment
    -- write each as comments


--Part 1: List the columns and their data types in the Jobs table.
-- Id INTEGER PRIMARY KEY AUTO_INCREMENT
-- Name LONGTEXT
-- EmployerId INTEGER

--Part 2: Write a query to list the names of the employers in St. Louis City.
-- SELECT Name 
-- From techjobs.employers
-- WHERE Location = "Saint Louis";

--Part 3: Write a query to return a list of the names and descriptions of all skills that are attached to jobs in alphabetical order.
    --If a skill does not have a job listed, it should not be included in the results of this query.
--SELECT SkillName
-- FROM techjobs.skills
-- INNER JOIN techjobs.jobskills on techjobs.skills.Id = techjobs.jobskills.SkillsId
-- WHERE techjobs.skills.Id IS NOT NULL;