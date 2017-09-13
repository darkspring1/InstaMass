START TRANSACTION;

SELECT migrations.set_migration(6);

--DELETE FROM "Accounts";

ALTER TABLE "Accounts" DROP CONSTRAINT "Accounts_pkey";
ALTER TABLE "Accounts" ADD COLUMN  "Id" UUID NOT NULL;
ALTER TABLE "Accounts" ADD CONSTRAINT "Accounts_pkey" PRIMARY KEY ("Id");
ALTER TABLE "Accounts" ADD CONSTRAINT "Accounts_unique_login" UNIQUE ("Login");

CREATE TABLE public."TaskTypes"
(
    "Id" INT NOT NULL,
    "Type" character varying(100) NOT NULL,
    CONSTRAINT "TaskTypes_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE public."Tasks"
(
    "Id" UUID NOT NULL,
    "TypeId" int NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
	"AccountId" UUID NOT NULL,
    CONSTRAINT "Tasks_pkey" PRIMARY KEY ("Id"),
   CONSTRAINT "FK_Tasks_Accounts.Id" FOREIGN KEY ("AccountId")
        REFERENCES public."Accounts" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE CASCADE,

        CONSTRAINT "FK_Tasks_TaskTypes.Id" FOREIGN KEY ("TypeId")
        REFERENCES public."TaskTypes" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE CASCADE
);

CREATE TABLE public."LikeTasks"
(
    "TaskId" UUID NOT NULL,
    "Tags" TEXT NOT NULL,
    CONSTRAINT "LikeTasks_pkey" PRIMARY KEY ("TaskId"),
   CONSTRAINT "FK_LikeTasks_Tasks.Id" FOREIGN KEY ("TaskId")
        REFERENCES public."Users" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION ON DELETE CASCADE
);

--ROLLBACK TRANSACTION;
COMMIT TRANSACTION;