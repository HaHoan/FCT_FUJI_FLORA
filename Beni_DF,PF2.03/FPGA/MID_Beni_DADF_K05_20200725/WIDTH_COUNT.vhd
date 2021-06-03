LIBRARY ieee;
USE ieee.std_logic_1164.all;
USE ieee.std_logic_unsigned.all;
use IEEE.std_logic_arith.all;

ENTITY WIDTH_COUNT IS
-- {{ALTERA_IO_BEGIN}} DO NOT REMOVE THIS LINE!
PORT
(
	RESET		:	IN		STD_LOGIC;
	START		:	IN		STD_LOGIC;
	WDGATE	:	IN		STD_LOGIC;
	WDTHCLK	:	IN		STD_LOGIC;
	CLKI		:	IN		STD_LOGIC;

	WDTHCNT	:	OUT	STD_LOGIC_VECTOR ( 31 downto 0 );
	WDTHHI	:	OUT	STD_LOGIC_VECTOR ( 31 downto 0 )
);

-- {{ALTERA_IO_END}} DO NOT REMOVE THIS LINE!
END WIDTH_COUNT;


--  Architecture Body

ARCHITECTURE RTL OF WIDTH_COUNT IS
	signal COUNT		: STD_LOGIC_VECTOR ( 31 downto 0 );
	signal COUNTHI		: STD_LOGIC_VECTOR ( 31 downto 0 );
	
BEGIN
	
	WDTHCNT	<= COUNT;
	WDTHHI	<= COUNTHI; 
	
	process (
		RESET,
		START,
		WDGATE,
		WDTHCLK,
		CLKI
	)
	begin
		if ( ( RESET = '0' ) or ( START = '0' ) ) then
			COUNT		<= ( others => '0' );
			COUNTHI	<= ( others => '0' );
		
		elsif ( WDTHCLK'event and WDTHCLK = '1' ) then
			if ( WDGATE = '1' ) then
				COUNT <= COUNT + '1';
				
				if ( CLKI = '1' ) then
					COUNTHI <= COUNTHI + '1';
				else
					NULL;
				end if;
			else
				NULL;
			end if;
		else
			NULL;
		end if;
	end process;

END RTL;