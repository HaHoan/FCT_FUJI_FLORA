LIBRARY ieee;
USE ieee.std_logic_1164.all;
USE ieee.std_logic_unsigned.all;
use IEEE.std_logic_arith.all;

ENTITY FILTER IS
	-- {{ALTERA_IO_BEGIN}} DO NOT REMOVE THIS LINE!
	PORT
	(
		RESET : IN STD_LOGIC;
		FILTERCNT : IN STD_LOGIC_VECTOR(3 downto 0);
		FILTERCLK : IN STD_LOGIC;
		FREQ : IN STD_LOGIC;
		FFREQ : OUT STD_LOGIC
	);
	-- {{ALTERA_IO_END}} DO NOT REMOVE THIS LINE!
END FILTER;


--  Architecture Body

ARCHITECTURE RTL OF FILTER IS
	
	signal	FCOUNT	: std_logic_vector ( 3 downto 0 );
	
BEGIN
	
	process (
		RESET,
		FILTER,
		FILTERCLK,
		FREQ
	)
	begin
		if ( RESET = '0' ) then
			FFREQ		<= '0';
			FCOUNT	<= (others => '0' );
		elsif ( FILTER = 0 ) then
			FFREQ <= FREQ;
		elsif ( FILTERCLK'event and FILTERCLK = '1' ) then
			if ( FREQ = '0' ) then
				FFREQ		<= '0';
				FCOUNT	<= ( others => '0' );
			else
			-- Hi に変化した時�みのフィルタ
				if ( FCOUNT < FILTER ) then
					FFREQ <= '0';
				else
					FFREQ <= '1';
				end if;
			end if;
		else
			NULL;
		end if;
	end process;
	
END RTL;
 