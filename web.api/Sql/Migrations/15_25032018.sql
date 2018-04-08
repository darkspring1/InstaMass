START TRANSACTION;
SELECT migrations.set_migration(15);

ALTER TABLE public."TaskActions" ADD COLUMN IsSuccess BOOLEAN NOT NULL;
ALTER TABLE public."TaskActions" ADD COLUMN StartedAt TIMESTAMP NOT NULL;

CREATE TABLE "public"."TagTaskActions" (
	"ActionId" uuid NOT NULL,
	"Tag" text not null,
	"MediaId" text not null,
	
	PRIMARY KEY ("ActionId"),
	FOREIGN KEY ("ActionId") REFERENCES "TaskActions"("Id") ON DELETE CASCADE
);


COMMIT TRANSACTION;