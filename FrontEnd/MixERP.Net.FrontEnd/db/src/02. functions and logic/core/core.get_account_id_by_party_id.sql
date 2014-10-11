CREATE FUNCTION core.get_account_id_by_party_id(party_id bigint)
RETURNS bigint
AS
$$
BEGIN
    RETURN
    (
        SELECT account_id
        FROM core.parties
        WHERE core.parties.party_id=$1
    );
END
$$
LANGUAGE plpgsql;
