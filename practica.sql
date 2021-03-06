PGDMP         !                y            school    10.16    13.3 #    )           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            *           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            +           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ,           1262    41502    school    DATABASE     c   CREATE DATABASE school WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';
    DROP DATABASE school;
                postgres    false            ?            1255    57886    update_marks()    FUNCTION     <  CREATE FUNCTION public.update_marks() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
begin
update result_test_math set five = cast (tg_argv[1] as integer),
four = cast (tg_argv[2] as integer), three = cast (tg_argv[3] as integer),
two = cast (tg_argv[4] as integer) where id = cast (tg_argv[0] as integer);
end;
$$;
 %   DROP FUNCTION public.update_marks();
       public          postgres    false            ?            1259    41550    autorizacia    TABLE     i   CREATE TABLE public.autorizacia (
    login character varying(25),
    password character varying(25)
);
    DROP TABLE public.autorizacia;
       public            postgres    false            ?            1259    41503    classes    TABLE     a   CREATE TABLE public.classes (
    id integer NOT NULL,
    name character varying(5) NOT NULL
);
    DROP TABLE public.classes;
       public            postgres    false            ?            1259    41506    quarter_four    TABLE     ?   CREATE TABLE public.quarter_four (
    id integer NOT NULL,
    percent_learning integer NOT NULL,
    quality_learning integer NOT NULL,
    unlearning integer NOT NULL
);
     DROP TABLE public.quarter_four;
       public            postgres    false            ?            1259    41509    quarter_one    TABLE     ?   CREATE TABLE public.quarter_one (
    id integer NOT NULL,
    percent_learning integer NOT NULL,
    quality_learning integer NOT NULL,
    unlearning integer NOT NULL
);
    DROP TABLE public.quarter_one;
       public            postgres    false            ?            1259    41512    quarter_three    TABLE     ?   CREATE TABLE public.quarter_three (
    id integer NOT NULL,
    percent_learning integer NOT NULL,
    quality_learning integer NOT NULL,
    unlearning integer NOT NULL
);
 !   DROP TABLE public.quarter_three;
       public            postgres    false            ?            1259    41515    quarter_two    TABLE     ?   CREATE TABLE public.quarter_two (
    id integer NOT NULL,
    percent_learning integer NOT NULL,
    quality_learning integer NOT NULL,
    unlearning integer NOT NULL
);
    DROP TABLE public.quarter_two;
       public            postgres    false            ?            1259    41553    result_for_year    TABLE     ?   CREATE TABLE public.result_for_year (
    id integer NOT NULL,
    percent_learning integer NOT NULL,
    quality_learning integer NOT NULL,
    unlearning integer NOT NULL
);
 #   DROP TABLE public.result_for_year;
       public            postgres    false            ?            1259    41521    result_test_math    TABLE     ?   CREATE TABLE public.result_test_math (
    id integer NOT NULL,
    class_id integer NOT NULL,
    teacher_id integer NOT NULL,
    five integer NOT NULL,
    four integer NOT NULL,
    three integer NOT NULL,
    two integer NOT NULL
);
 $   DROP TABLE public.result_test_math;
       public            postgres    false            ?            1259    41524    result_test_russ    TABLE     ?   CREATE TABLE public.result_test_russ (
    id integer NOT NULL,
    class_id integer NOT NULL,
    teacher_id integer NOT NULL,
    five integer NOT NULL,
    four integer NOT NULL,
    three integer NOT NULL,
    two integer NOT NULL
);
 $   DROP TABLE public.result_test_russ;
       public            postgres    false            ?            1259    41527    teachers    TABLE     d   CREATE TABLE public.teachers (
    id integer NOT NULL,
    name character varying(255) NOT NULL
);
    DROP TABLE public.teachers;
       public            postgres    false            %          0    41550    autorizacia 
   TABLE DATA           6   COPY public.autorizacia (login, password) FROM stdin;
    public          postgres    false    204   U'                 0    41503    classes 
   TABLE DATA           +   COPY public.classes (id, name) FROM stdin;
    public          postgres    false    196   {'                 0    41506    quarter_four 
   TABLE DATA           Z   COPY public.quarter_four (id, percent_learning, quality_learning, unlearning) FROM stdin;
    public          postgres    false    197   ?'                 0    41509    quarter_one 
   TABLE DATA           Y   COPY public.quarter_one (id, percent_learning, quality_learning, unlearning) FROM stdin;
    public          postgres    false    198   Y(                  0    41512    quarter_three 
   TABLE DATA           [   COPY public.quarter_three (id, percent_learning, quality_learning, unlearning) FROM stdin;
    public          postgres    false    199   ?(       !          0    41515    quarter_two 
   TABLE DATA           Y   COPY public.quarter_two (id, percent_learning, quality_learning, unlearning) FROM stdin;
    public          postgres    false    200   R)       &          0    41553    result_for_year 
   TABLE DATA           ]   COPY public.result_for_year (id, percent_learning, quality_learning, unlearning) FROM stdin;
    public          postgres    false    205   ?)       "          0    41521    result_test_math 
   TABLE DATA           \   COPY public.result_test_math (id, class_id, teacher_id, five, four, three, two) FROM stdin;
    public          postgres    false    201   F*       #          0    41524    result_test_russ 
   TABLE DATA           \   COPY public.result_test_russ (id, class_id, teacher_id, five, four, three, two) FROM stdin;
    public          postgres    false    202   ?*       $          0    41527    teachers 
   TABLE DATA           ,   COPY public.teachers (id, name) FROM stdin;
    public          postgres    false    203   ?+       ?
           2606    41533    classes classes_name_key 
   CONSTRAINT     S   ALTER TABLE ONLY public.classes
    ADD CONSTRAINT classes_name_key UNIQUE (name);
 B   ALTER TABLE ONLY public.classes DROP CONSTRAINT classes_name_key;
       public            postgres    false    196            ?
           2606    41531    classes classes_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.classes
    ADD CONSTRAINT classes_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.classes DROP CONSTRAINT classes_pkey;
       public            postgres    false    196            ?
           2606    41535    quarter_four quarter_four_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.quarter_four
    ADD CONSTRAINT quarter_four_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.quarter_four DROP CONSTRAINT quarter_four_pkey;
       public            postgres    false    197            ?
           2606    41537    quarter_one quarter_one_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.quarter_one
    ADD CONSTRAINT quarter_one_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.quarter_one DROP CONSTRAINT quarter_one_pkey;
       public            postgres    false    198            ?
           2606    41539     quarter_three quarter_three_pkey 
   CONSTRAINT     ^   ALTER TABLE ONLY public.quarter_three
    ADD CONSTRAINT quarter_three_pkey PRIMARY KEY (id);
 J   ALTER TABLE ONLY public.quarter_three DROP CONSTRAINT quarter_three_pkey;
       public            postgres    false    199            ?
           2606    41541    quarter_two quarter_two_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.quarter_two
    ADD CONSTRAINT quarter_two_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.quarter_two DROP CONSTRAINT quarter_two_pkey;
       public            postgres    false    200            ?
           2606    41545 &   result_test_math result_test_math_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.result_test_math
    ADD CONSTRAINT result_test_math_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.result_test_math DROP CONSTRAINT result_test_math_pkey;
       public            postgres    false    201            ?
           2606    41547 &   result_test_russ result_test_russ_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.result_test_russ
    ADD CONSTRAINT result_test_russ_pkey PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.result_test_russ DROP CONSTRAINT result_test_russ_pkey;
       public            postgres    false    202            ?
           2606    41549    teachers teachers_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.teachers
    ADD CONSTRAINT teachers_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.teachers DROP CONSTRAINT teachers_pkey;
       public            postgres    false    203            ?
           2620    57887    result_test_math marks    TRIGGER     t   CREATE TRIGGER marks BEFORE DELETE ON public.result_test_math FOR EACH ROW EXECUTE PROCEDURE public.update_marks();
 /   DROP TRIGGER marks ON public.result_test_math;
       public          postgres    false    201    206            %      x?KL????L?\1z\\\ 4?         S   x??I?@???????k&??Ӈ???0??fr??????c&/?[?JJXua?:vݰ|?nY??[w?d4Hԅ??AZ'?         k   x?U???0??0L9ݥ??Q??*R??d\l?m'vq3?s?_?r@,fZA~+7??Ar?q?h?ݥ@??D?̳d?j????}+"??}.U} u%?         n   x?U?I?  ???p????G????DY2??>Z?ͤ??hM?@???O;0nz"a|3?dܠ9?e?8m??w??,????v?ܿ&aGe??c 2??X̹??R??'&_          k   x?M?A!D?uq?? (ޥ???p?????ǈ??#??:?͙ŸՋ??u7c?V??_??vfU?BL?:ר<?u??ܰE?-z?????????q~?^k???| ?f      !   j   x?-???0Dѳ)&b??K??#??'???<??7?????8???Ł%??g??I^?r\+8ȜF???UĦ?&?&.??حT?ڣ,j???oC???}3? *??      &   j   x?-???0Dѳ)&b??K??#??'???<??7?????8???Ł%??g??I^?r\+8ȜF???UĦ?&?&.??حT?ڣ,j???oC???}3? *??      "   ?   x?5??C1?PL???????:??_]X	???[???T ???VT?i/I?(???R?? ?΢???u:p??`+?R?	?ߥ????T?䧴Dt??=????A$??Tz?D?n@us[ʻ
?????¿w?zx'      #   ?   x?EO[!????H}?e???dR?HE??`5?m%?`lh??F?Z7X=??pa?z??RM?:|???w?|???pٰilo?Ry?٘????k???l?\?39ل/??0?z[2V?N?o??2??I#??v???????(?y~?Q?)?      $   ?  x?uSIn?@<ۯ?"Y??ǘ%?BD? EdE?uD?4`??B??R?ㅀ#K֌{???r?NhG9?#z??R?q?9?p??]??'?-?%?ð?ņ??B9zi??=zq?=?оJ?4??(}"zT????H끰F??(9??R??󝧸???2@ζ	??e????>Y))@?dU?e@o???LDKp 6?3?F??*@F??e??ɉ6?"z???L5?u@??C?????4???I??>i?i??u2?DD?T?9??$??? ű,?_?4???5f?H?G+ɘ??b\D?u???Y05?m?<?	;+kJ?q?T-,???j??	2?-?ps|{/"?,0J?|?d??U?#?̇梅.%-W
??(????:w*&??;?9\??qi}-?+?P??+7*???LC??n?V????G<?M?z?v?"??=???'?V     