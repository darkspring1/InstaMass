START TRANSACTION;
SELECT migrations.set_migration(15);



CREATE TABLE "public"."TagTaskActions" (
	"ActionId" uuid NOT NULL,
	"Tag" text not null,
	"MediaPk" text not null,
	"MediaCode" text not null,
	"MediaUrl" text not null,
	
	PRIMARY KEY ("ActionId"),
	FOREIGN KEY ("ActionId") REFERENCES "TaskActions"("Id") ON DELETE CASCADE
);


COMMIT TRANSACTION;