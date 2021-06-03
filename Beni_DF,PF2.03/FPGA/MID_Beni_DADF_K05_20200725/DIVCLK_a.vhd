LIBRARY ieee;
USE ieee.std_logic_1164.all;
USE ieee.std_logic_unsigned.all;
use IEEE.std_logic_arith.all;

ENTITY DIVCLK IS
-- {{ALTERA_IO_BEGIN}} DO NOT REMOVE THIS LINE!
PORT
(
	RESET		:	IN		STD_LOGIC;
	CLKIN		:	IN		STD_LOGIC;
	DIV		:	IN		STD_LOGIC_VECTOR ( 15 downto 0 );	-- １周期を制定( = CLKIN x DIV )
	
	
	CLKOUT	:	OUT	STD_LOGIC		-- OUTPUT Clock
);

-- {{ALTERA_IO_END}} DO NOT REMOVE THIS LINE!
END DIVCLK;


--  Architecture Body

ARCHITECTURE RTL OF DIVCLK IS
	
	signal	COUNT	: std_logic_vector ( 15 downto 0 );
	
BEGIN
	
	process (
		RESET,
		CLKIN,
		DIV
	)
	begin
		if ( RESET = '0' ) then
			COUNT		<= (others=>'0');
			CLKOUT	<= '0';
		elsif ( CLKIN'event and CLKIN = '1' ) then
			
			if ( COUNT <  CONV_STD_LOGIC_VECTOR( ( CONV_INTEGER( DIV ) / 2 ), 16 ) ) then
				COUNT		<= COUNT + '1';
				CLKOUT	<= '1';
			elsif ( COUNT < DIV ) then
				COUNT		<= COUNT + '1';
				CLKOUT	<= '0';
			else
				COUNT		<= X"0001";
				CLKOUT	<= '1';
			end if;
		
		else
			NULL;
		end if;
	end process;
	
END RTL;