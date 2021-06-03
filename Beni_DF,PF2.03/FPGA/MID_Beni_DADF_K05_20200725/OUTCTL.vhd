LIBRARY ieee;
USE ieee.std_logic_1164.all;
USE ieee.std_logic_unsigned.all;

ENTITY OUTCTL IS
	-- {{ALTERA_IO_BEGIN}} DO NOT REMOVE THIS LINE!
	PORT
	(
		INP		: IN	STD_LOGIC;
		INV		: IN	STD_LOGIC;
		PP			: IN	STD_LOGIC;
		nOE		: IN	STD_LOGIC;
		CKO		: IN	STD_LOGIC;
		OUTP		: OUT	STD_LOGIC
	);
	-- {{ALTERA_IO_END}} DO NOT REMOVE THIS LINE!
END OUTCTL;


--  Architecture Body

ARCHITECTURE RTL OF OUTCTL IS


BEGIN

	process (
		INP,
		INV,
		PP,
		nOE,
		CKO
	)
	begin
		if ( nOE = '1' ) then
			OUTP <= 'Z';
		else
			if ( PP = '1' ) then
				if ( INV = '0' ) then
					OUTP <= ( INP or CKO );
				else
					OUTP <= NOT ( INP or CKO );
				end if;
			else
				if ( ( INP or CKO ) = '1' ) then
					if ( INV = '0' ) then
						OUTP <= 'Z';
					else
						OUTP <= '0';
					end if;
				else
					if ( INV = '0' ) then
						OUTP <= '0';
					else
						OUTP <= 'Z';
					end if;
				end if;
			end if;
		end if;
	end process;

	
END RTL;
