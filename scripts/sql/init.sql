CREATE EXTENSION "uuid-ossp";

create table users (
	user_id SERIAL not null,
	email varchar(500) not null unique,
	name text not null,
	passphrase text not null,
	active bool not null default true,
	force_password_reset bool not null default false,
	created_at TIMESTAMP not null default now(),
	deleted_at TIMESTAMP,
	
	primary key(user_id)
);

create table enterprises (
	enterprise_id SERIAL not null,
	name varchar(500),
	active bool not null default true,
	created_at TIMESTAMP not null default now(),
	deleted_at TIMESTAMP,
	primary key (enterprise_id)
);

create table codes (
	code_id SERIAL not null,
	code VARCHAR(9) not null,
	expire_in TIMESTAMP not null,
	created_at TIMESTAMP default now(),
	user_id bigint not null,
	
	primary key(code_id),
	foreign key (user_id) references users(user_id)
);

create table permissions (
	permission_id SERIAL not null,
	name varchar(500) not null,
	primary key(permission_id)
);

create table roles (
	role_id SERIAL not null,
	name varchar(500) not null,
	primary key(role_id)
);

create table role_permission (
	permission_id bigint not null,
	role_id bigint  not null,
	primary key(permission_id, role_id),
	foreign key(permission_id) references permissions(permission_id),
	foreign key(role_id) references roles(role_id)
);

create table user_role (
	user_id bigint  not null,
	role_id bigint not null,
	primary key(user_id, role_id),
	foreign key(role_id) references roles(role_id),
	foreign key(user_id) references users(user_id)
);

create table user_enterprise (
	user_id bigint not null,
	enterprise_id bigint not null,
	primary key(user_id, enterprise_id),
	foreign key(enterprise_id) references enterprises(enterprise_id),
	foreign key(user_id) references users(user_id)
);

create view document$type as (
	select 1 as document_type, 'CPF' as document_name UNION
	select 2 as document_type, 'CNPJ' as document_name UNION
	select 3 as document_type, 'RG' as document_name UNION
	select 4 as document_type, 'CNH' as document_name UNION
	select 5 as document_type, 'PASSAPORTE' as document_name UNION
	select 6 as document_type, 'TITULO ELEITOR' as document_name UNION
	select 7 as document_type, 'CTPS' as document_name UNION
	select 8 as document_type, 'CERTIDÃO NASCIMENTO' as document_name
);

create view contact$type as (
	select 1 as contact_type, 'TELEFONE CELULAR' as document_name union
	select 2 as contact_type, 'TELEFONE RESIDENCIAL' as document_name UNION
	select 3 as contact_type, 'EMAIL' as document_name
);

create view address$type as (
	select 1 as address_type, 'PAÍS' as document_name union
	select 2 as address_type, 'ESTADO' as document_name UNION
	select 3 as address_type, 'CIDADE' as document_name union
	select 4 as address_type, 'RUA' as document_name union
	select 5 as address_type, 'CEP' as document_name union
	select 6 as address_type, 'NUMERO' as document_name union
	select 7 as address_type, 'COMPLEMENTO' as document_name
);

create table persons (
	person_id SERIAL not null,
	name varchar(500) not null,
	birth_date TIMESTAMP not null,
	created_at TIMESTAMP not null default now(),
	deleted_at TIMESTAMP,
	
	primary key(person_id)
);

create table documents (
	id UUID DEFAULT uuid_generate_v4(),
	person_id bigint not null,
	document varchar(30) not null,
	document_type int not null,
	created_at TIMESTAMP not null default now(),
	deleted_at TIMESTAMP,
	
	primary key (id),
	foreign key (person_id) references persons(person_id)
);

create table contacts (
	id UUID DEFAULT uuid_generate_v4(),
	person_id bigint not null,
	value varchar(500) not null,
	contact_type int not null,
	created_at TIMESTAMP not null default now(),
	deleted_at TIMESTAMP,
	primary key (id),
	foreign key (person_id) references persons(person_id)
);

create table addresses (
	id UUID DEFAULT uuid_generate_v4(),
	person_id bigint not null,
	value varchar(500) not null,
	address_type int not null,
	created_at TIMESTAMP not null default now(),
	deleted_at TIMESTAMP,
	primary key (id),
	foreign key (person_id) references persons(person_id)
);