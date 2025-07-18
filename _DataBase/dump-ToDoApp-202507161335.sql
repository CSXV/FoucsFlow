PGDMP  7    #                }            ToDoApp    17.5    17.5     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    16408    ToDoApp    DATABASE     u   CREATE DATABASE "ToDoApp" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.UTF-8';
    DROP DATABASE "ToDoApp";
                     postgres    false            �            1259    16453 
   categories    TABLE     �   CREATE TABLE public.categories (
    id integer NOT NULL,
    name character varying(100) NOT NULL,
    iconname character varying NOT NULL
);
    DROP TABLE public.categories;
       public         heap r       user    false            �            1259    16452    categories_id_seq    SEQUENCE     �   CREATE SEQUENCE public.categories_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.categories_id_seq;
       public               user    false    220            �           0    0    categories_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.categories_id_seq OWNED BY public.categories.id;
          public               user    false    219            �            1259    16460    notes    TABLE     �  CREATE TABLE public.notes (
    id integer NOT NULL,
    title character varying(255) NOT NULL,
    content text,
    userid integer NOT NULL,
    categoryid integer NOT NULL,
    createdate timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updatedate timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    state character varying(20) DEFAULT 'To Do'::character varying NOT NULL,
    ispinned boolean DEFAULT false NOT NULL
);
    DROP TABLE public.notes;
       public         heap r       user    false            �            1259    16459    notes_id_seq    SEQUENCE     �   CREATE SEQUENCE public.notes_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.notes_id_seq;
       public               user    false    222            �           0    0    notes_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.notes_id_seq OWNED BY public.notes.id;
          public               user    false    221            �            1259    16436    users    TABLE     ;  CREATE TABLE public.users (
    id integer NOT NULL,
    username character varying(100) NOT NULL,
    email character varying(255) NOT NULL,
    passwordhash character varying(255) NOT NULL,
    firstname character varying(100) NOT NULL,
    lastname character varying(100) NOT NULL,
    createdate timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    updatedate timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    isactive boolean DEFAULT true NOT NULL,
    profileimage character varying(255),
    usertype integer DEFAULT 0 NOT NULL
);
    DROP TABLE public.users;
       public         heap r       user    false            �            1259    16435    users_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.users_id_seq;
       public               user    false    218            �           0    0    users_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;
          public               user    false    217            �           2604    16456    categories id    DEFAULT     n   ALTER TABLE ONLY public.categories ALTER COLUMN id SET DEFAULT nextval('public.categories_id_seq'::regclass);
 <   ALTER TABLE public.categories ALTER COLUMN id DROP DEFAULT;
       public               user    false    219    220    220            �           2604    16463    notes id    DEFAULT     d   ALTER TABLE ONLY public.notes ALTER COLUMN id SET DEFAULT nextval('public.notes_id_seq'::regclass);
 7   ALTER TABLE public.notes ALTER COLUMN id DROP DEFAULT;
       public               user    false    221    222    222            �           2604    16439    users id    DEFAULT     d   ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);
 7   ALTER TABLE public.users ALTER COLUMN id DROP DEFAULT;
       public               user    false    218    217    218            }          0    16453 
   categories 
   TABLE DATA           8   COPY public.categories (id, name, iconname) FROM stdin;
    public               user    false    220   �                  0    16460    notes 
   TABLE DATA           p   COPY public.notes (id, title, content, userid, categoryid, createdate, updatedate, state, ispinned) FROM stdin;
    public               user    false    222   E!       {          0    16436    users 
   TABLE DATA           �   COPY public.users (id, username, email, passwordhash, firstname, lastname, createdate, updatedate, isactive, profileimage, usertype) FROM stdin;
    public               user    false    218   �"       �           0    0    categories_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.categories_id_seq', 7, true);
          public               user    false    219            �           0    0    notes_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.notes_id_seq', 17, true);
          public               user    false    221            �           0    0    users_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.users_id_seq', 13, true);
          public               user    false    217            �           2606    16458    categories categories_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.categories DROP CONSTRAINT categories_pkey;
       public                 user    false    220            �           2606    16471    notes notes_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.notes
    ADD CONSTRAINT notes_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.notes DROP CONSTRAINT notes_pkey;
       public                 user    false    222            �           2606    16447    users users_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public                 user    false    218            �           2606    16449    users users_username_key 
   CONSTRAINT     W   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);
 B   ALTER TABLE ONLY public.users DROP CONSTRAINT users_username_key;
       public                 user    false    218            �           2606    16477    notes notes_categoryid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.notes
    ADD CONSTRAINT notes_categoryid_fkey FOREIGN KEY (categoryid) REFERENCES public.categories(id) ON DELETE SET NULL;
 E   ALTER TABLE ONLY public.notes DROP CONSTRAINT notes_categoryid_fkey;
       public               user    false    222    3300    220            �           2606    16472    notes notes_userid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.notes
    ADD CONSTRAINT notes_userid_fkey FOREIGN KEY (userid) REFERENCES public.users(id) ON DELETE CASCADE;
 A   ALTER TABLE ONLY public.notes DROP CONSTRAINT notes_userid_fkey;
       public               user    false    222    3296    218            }   z   x�-�A
�0F��?��V� .A�M!L�4�I3%Soon��[�-n���1K5-�]ρ<�&|���?a����=�l��f1�I�B-�Oa�L:F��(~1:��cf�8�k舓r6y�t"��o)�         P  x�]�=o�0�g�+޵X�go��Х����%D
6JL#�}A|I^�'�{�c�	��]; q���d��fQ�� �S��5�M!����pZ:]smL�%[RG�����5c*Ѵ=�Sw¦�{���	#��s�����n�����v���6aO��"��M�������%1��9v�/����A�O8t��0��!��1N����Y9U:SqQW��,�7�X^o�y�>^8�鷕r�P�I�٩rZqk+%��V��BWU~���r�a��0?�\$^u����z{�U�&���a�p������/��ke�~�d!�r�	�k%Tuw��g��?i���      {   �   x�u�;N1@��)�;�'N�%'�&���
q�i$7�~~���L1�K���1?��������N}HPik�s�qR���>*���ɇ�C�ֺ�C�z�L$W�W��t�%[���\Ƞ
��t{O�!���X�g��bk�T�Aa������j��*v��y]������{~��� ۭZU����v۷m��K�     