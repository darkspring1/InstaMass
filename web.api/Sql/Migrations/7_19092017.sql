--likeTaskFix
START TRANSACTION;

SELECT migrations.set_migration(7);

ALTER TABLE public."LikeTasks" DROP CONSTRAINT "FK_LikeTasks_Tasks.Id";

ALTER TABLE public."LikeTasks" ADD  CONSTRAINT "FK_LikeTasks_Tasks.Id" FOREIGN KEY ("TaskId")
        REFERENCES public."Tasks" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE CASCADE;

--ROLLBACK TRANSACTION;
COMMIT TRANSACTION;