DROP FUNCTION IF EXISTS office.can_login(user_id integer_strict, office_id integer_strict);
CREATE FUNCTION office.can_login(user_id integer_strict, office_id integer_strict)
RETURNS boolean
AS
$$
DECLARE _office_id integer;
BEGIN
	_office_id:=office.get_office_id_by_user_id($1);

	IF $1 = office.get_sys_user_id() THEN
		RETURN false;
	END IF;

	IF $2=_office_id THEN
		RETURN true;
	ELSE
		IF office.is_parent_office(_office_id,$2) THEN
			RETURN true;
		END IF;
	END IF;
	RETURN false;
END;
$$
LANGUAGE plpgsql;
