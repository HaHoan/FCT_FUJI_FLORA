LIBRARY ieee;
USE ieee.std_logic_1164.all;
USE ieee.std_logic_unsigned.all;
use IEEE.std_logic_arith.all;

ENTITY FREQUENCY IS
	-- {{ALTERA_IO_BEGIN}} DO NOT REMOVE THIS LINE!
	PORT
	(
		RESET 	: IN	STD_LOGIC;
		START		: IN	STD_LOGIC;
		VALIDF	: IN	STD_LOGIC;
		FLTFREQ	: IN	STD_LOGIC;
		FREQCNT	: OUT	STD_LOGIC_VECTOR(15 downto 0)
	);
	-- {{ALTERA_IO_END}} DO NOT REMOVE THIS LINE!
END FREQUENCY;


--  Architecture Body

ARCHITECTURE RTL OF FREQUENCY IS
	signal COUNT		: STD_LOGIC_VECTOr ( 15 downto 0 );
	
BEGIN
	
	FREQCNT <= COUNT;
	
	process (
		RESET,
		START,
		FLTFREQ,
		VALIDF
	)
	begin
		if ( ( RESET = '0' ) or ( START = '0' ) ) then
			COUNT <= ( others => '0' );
		
		elsif ( FLTFREQ'event and FLTFREQ = '1' ) then
			if ( VALIDF = '1' ) then
				COUNT <= COUNT + '1';
			else
				NULL;
			end if;
		else
			NULL;
		end if;
	end process;

END RTL;
