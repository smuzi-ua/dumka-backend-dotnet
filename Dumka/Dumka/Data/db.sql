create table report_categories
(
    id		int IDENTITY(1,1)				primary key,
    name	varchar(256)		not null
);

alter table report_categories
add	  date_created	datetime		default CURRENT_TIMESTAMP	not null,
	  date_modified	datetime		default CURRENT_TIMESTAMP	not null;

create table schools
(
    id      int IDENTITY(1,1)				primary key,
    name    varchar(256)		null,
    display bit					not null,
    constraint schools_name_uindex unique (name)
);

alter table schools
add	  date_created	datetime		default CURRENT_TIMESTAMP	not null,
	  date_modified	datetime		default CURRENT_TIMESTAMP	not null;

create table user_types
(
	id		int IDENTITY(1,1)				primary key,
	name	char(10)			not null
)

INSERT INTO user_types (name)
VALUES ('student'), ('supervisor'), ('teacher'), ('headmaster')

create table users
(
    id             int IDENTITY(1,1)		primary key,
    school_id      int                                                                               not null,
    name           varchar(128)                                                                      not null,
    nickname       varchar(32)                                                                       not null,
    code           int(6)                                    default(floor(rand() * 900000 + 100000)) null,
    code_generated datetime                                  default CURRENT_TIMESTAMP				 null,
    user_type_id   int										 default 0								 not null,
    banned_to      datetime                                                                          null,
    verified       bit										 default 0                               null,
    constraint users_nickname_uindex
        unique (nickname),
    constraint users_schools_id_fk
        foreign key (school_id) references schools (id),
    constraint users_types_id_fk
        foreign key (user_type_id) references user_types (id)
);

alter table users
add	  date_created	datetime		default CURRENT_TIMESTAMP	not null,
	  date_modified	datetime		default CURRENT_TIMESTAMP	not null;

create table proposal_deadline_types
(
	id		int IDENTITY(1,1)				primary key,
	name	char(5)			not null
)

INSERT INTO proposal_deadline_types (name)
VALUES ('day'), ('2day'), ('week'), ('month')

create table proposal_stage_types
(
	id		int IDENTITY(1,1)				primary key,
	name	char(11)			not null
)

INSERT INTO proposal_stage_types (name)
VALUES ('none'), ('active'), ('in_progress'), ('archive')

create table proposals
(
    id				int IDENTITY(1,1)		primary key,
    user_id			int                                                                     not null,
    anonymous		bit																		not null,
    title			varchar(512)															not null,
    text			text																	not null,
    stage_id		int				default 0												null,
    deadline_id		int																		null,
    date_created	datetime		default CURRENT_TIMESTAMP								not null,
	date_modified	datetime		default CURRENT_TIMESTAMP								not null
    constraint proposals_users_id_fk
        foreign key (user_id) references users (id),
	constraint proposals_deadlines_id_fk
        foreign key (deadline_id) references proposal_deadline_types (id),
	constraint proposals_stages_id_fk
        foreign key (stage_id) references proposal_stage_types (id)
);

create table comments
(
    id          int IDENTITY(1,1)	primary key,
    proposal_id int                                   not null,
    user_id     int                                   not null,
    comment     text                                  not null,
    anonymous   bit									  not null,
    date_created	datetime		default CURRENT_TIMESTAMP	not null,
	date_modified	datetime		default CURRENT_TIMESTAMP	not null,
    constraint comments_proposals_id_fk
        foreign key (proposal_id) references proposals (id),
    constraint comments_users_id_fk
        foreign key (user_id) references users (id)
);

create table feedback_type
(
	id		int IDENTITY(1,1)				primary key,
	name	char(7)			not null
)

INSERT INTO feedback_types (name)
VALUES ('like'), ('dislike')

create table comment_likes
(
    id			  int IDENTITY(1,1)      primary key,
    comment_id    int										not null,
    user_id		  int										not null,
    feedback_id   int										not null,
    date_created  datetime		default CURRENT_TIMESTAMP	not null,
	date_modified datetime		default CURRENT_TIMESTAMP	not null,
    constraint comment_likes_comments_id_fk
        foreign key (comment_id) references comments (id),
    constraint comment_likes_users_id_fk
        foreign key (user_id) references users (id),
	constraint comment_likes_feedbacks_id_fk
        foreign key (feedback_id) references feedback_types (id)
);

create table comment_reports
(
    id				int IDENTITY(1,1)      primary key,
    comment_id      int										not null,
    user_id         int										not null,
    report_category int										not null,
    comment         text									null,
    date_created  datetime		default CURRENT_TIMESTAMP	not null,
	date_modified datetime		default CURRENT_TIMESTAMP	not null,
    constraint comment_reports_comments_id_fk
        foreign key (comment_id) references comments (id),
    constraint comment_reports_report_categories_id_fk
        foreign key (report_category) references report_categories (id),
    constraint comment_reports_users_id_fk
        foreign key (user_id) references users (id)
);

create table proposal_likes
(
    id				int IDENTITY(1,1)      primary key,
    proposal_id		int                                     not null,
    user_id			int                                     not null,
    feedback_id		int										not null,
    date_created  datetime		default CURRENT_TIMESTAMP	not null,
	date_modified datetime		default CURRENT_TIMESTAMP	not null,
    constraint proposal_likes_proposals_id_fk
        foreign key (proposal_id) references proposals (id),
    constraint proposal_likes_users_id_fk
        foreign key (user_id) references users (id),
	constraint proposal_likes_feedbacks_id_fk
        foreign key (feedback_id) references feedback_types (id)
);

create table proposal_reports
(
    id				int IDENTITY(1,1)      primary key,
    proposal_id     int										not null,
    user_id         int										not null,
    report_category int										not null,
    comment         text									null,
    date_created  datetime		default CURRENT_TIMESTAMP	not null,
	date_modified datetime		default CURRENT_TIMESTAMP	not null,
    constraint proposal_reports_proposals_id_fk
        foreign key (proposal_id) references proposals (id),
    constraint proposal_reports_report_categories_id_fk
        foreign key (report_category) references report_categories (id),
    constraint proposal_reports_users_id_fk
        foreign key (user_id) references users (id)
);