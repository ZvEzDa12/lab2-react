create table manufacturer (
    id bigserial primary key,
    name varchar(100) not null,
    country varchar(50),
    founded_year smallint
);

create table role (
    id smallserial primary key,
    name varchar(50) not null unique
);

create table vehicle_category (
    id smallserial primary key,
    name varchar(50) not null unique
);

create table fuel_type (
    id smallserial primary key,
    name varchar(30) not null unique
);

create table color (
    id serial primary key,
    name varchar(30) not null unique,
    hex_code varchar(7) not null
);

create table vehicle_model (
    id serial primary key,
    name varchar(40) not null,
    manufacturer_id bigint not null,
    vehicle_category_id smallint not null,
    power smallint not null,
    fuel_type_id smallint not null,
    load_capacity int not null,
    co2_emissions decimal(5,2),
    foreign key (manufacturer_id) references manufacturer(id),
    foreign key (vehicle_category_id) references vehicle_category(id),
    foreign key (fuel_type_id) references fuel_type(id)
);

create table vehicle (
    id bigserial primary key,
    model_id int not null,
    color_id int,
    vin varchar(17) unique not null,
    production_year smallint not null,
    mileage int default 0,
    registration_number varchar(20) unique,
    foreign key (model_id) references vehicle_model(id),
    foreign key (color_id) references color(id)
);

create table "user" (
    id bigserial primary key,
    login varchar(32) not null unique,
    password_hash varchar(32) not null,
    role_id smallint,
    first_name varchar(50),
    last_name varchar(50),
    middle_name varchar(50),
    passport_number varchar(20) unique,
    phone bigint,
    foreign key (role_id) references role(id)
);

create table ownership_history (
    id bigserial primary key,
    vehicle_id bigint not null,
    user_id bigint not null,
    start_date date not null,
    end_date date,
    foreign key (vehicle_id) references vehicle(id),
    foreign key (user_id) references "user"(id)
);

create table maintenance_type (
    id serial primary key,
    name varchar(100) not null unique
);

create table maintenance (
    id bigserial primary key,
    vehicle_id bigint not null,
    maintenance_type_id int not null,
    service_date date not null,
    cost decimal(12,2),
    description varchar(100),
    foreign key (vehicle_id) references vehicle(id),
    foreign key (maintenance_type_id) references maintenance_type(id)
);

create table insurance (
    id bigserial primary key,
    vehicle_id bigint not null,
    policy_number varchar(30) not null,
    company varchar(100) not null,
    start_date date not null,
    end_date date not null,
    cost decimal(10,2) not null,
    foreign key (vehicle_id) references vehicle(id)
);