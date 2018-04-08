START TRANSACTION;
SELECT migrations.set_migration(14);

CREATE TABLE public."TaskActions"(
	"Id" uuid NOT NULL,
	"TaskId" uuid NOT NULL,
	"StartedAt" TIMESTAMP NOT null,
	"FinishedAt" timestamp NOT null,
	"IsSuccess" BOOLEAN NOT null,
	CONSTRAINT "TaskAction_pkey" PRIMARY KEY ("Id"),
	FOREIGN KEY ("TaskId") REFERENCES "Tasks"("Id") ON DELETE CASCADE
);


COMMIT TRANSACTION;