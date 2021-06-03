LIBRARY ieee;
USE ieee.std_logic_1164.all;
USE ieee.std_logic_unsigned.all;
use IEEE.std_logic_arith.all;

ENTITY WIDTH_CTRL IS
-- {{ALTERA_IO_BEGIN}} DO NOT REMOVE THIS LINE!
PORT
(
	RESET		:	IN		STD_LOGIC;
	START		:	IN		STD_LOGIC;
	CLK		:	IN		STD_LOGIC;
	WIDTHMODE:	IN		STD_LOGIC_VECTOR ( 1 downto 0 );
	WIDTHSIG	:	IN		STD_LOGIC;
	
	WDSTART	:	OUT	STD_LOGIC;
	WDENd		:	OUT	STD_LOGIC;
	WDSTA		:	OUT	STD_LOGIC_VECTOR ( 1 downto 0 )
);

-- {{ALTERA_IO_END}} DO NOT REMOVE THIS LINE!
END WIDTH_CTRL;


--  Architecture Body

ARCHITECTURE RTL OF WIDTH_CTRL IS
	
	signal WIDTH_START		: STD_LOGIC;
	signal WIDTH_END			: STD_LOGIC;
	signal WIDTH_STA			: STD_LOGIC_VECTOR ( 1 downto 0 );
	
BEGIN
	
	process (
		RESET,
		START,
		CLK,
		WIDTHMODE,
		WIDTHSIG
	)
	begin
		if ( RESET = '0' or START = '0' ) then
			WIDTH_STA		<= "00";
			WIDTH_START		<= '0';
			WIDTH_END		<= '0';
		
		elsif ( CLK'event and CLK = '1' ) then
			case WIDTHMODE is
				when "00" =>	-- Up to Up
					if		( WIDTH_STA = "00" ) then
						if ( WIDTHSIG = '0' ) then
							WIDTH_START		<= '0';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "01";
						else
							NULL;
						end if;
					elsif ( WIDTH_STA = "01" ) then
						if ( WIDTHSIG = '1' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "11";
						else
							NULL;
						end if;
					elsif ( WIDTH_STA = "11" ) then
						if ( WIDTHSIG = '0' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "10";
						else
							NULL;
						end if;
					else -- 10
						if ( WIDTHSIG = '1' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '1';
							WIDTH_STA		<= "10";
						else
							NULL;
						end if;
					end if;
				when "01" =>	-- Up to Down
					if		( WIDTH_STA = "00" ) then
						if ( WIDTHSIG = '0' ) then
							WIDTH_START		<= '0';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "01";
						else
							NULL;
						end if;
					elsif ( WIDTH_STA = "01" ) then
						if ( WIDTHSIG = '1' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "11";
						else
							NULL;
						end if;
					else -- 11
						if ( WIDTHSIG = '1' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '1';
							WIDTH_STA		<= "11";
						else
							NULL;
						end if;
					end if;
				when "10" =>	-- Down to Up
					if		( WIDTH_STA = "00" ) then
						if ( WIDTHSIG = '1' ) then
							WIDTH_START		<= '0';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "01";
						else
							NULL;
						end if;
					elsif ( WIDTH_STA = "01" ) then
						if ( WIDTHSIG = '0' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "11";
						else
							NULL;
						end if;
					else	-- 11
						if ( WIDTHSIG = '1' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '1';
							WIDTH_STA		<= "11";
						else
							NULL;
						end if;
					end if;
				
				when "11" =>	-- Down to Down
					if		( WIDTH_STA = "00" ) then
						if ( WIDTHSIG = '1' ) then
							WIDTH_START		<= '0';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "01";
						else
							NULL;
						end if;
					elsif ( WIDTH_STA = "01" ) then
						if ( WIDTHSIG = '0' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "11";
						else
							NULL;
						end if;
					elsif ( WIDTH_STA = "11" ) then
						if ( WIDTHSIG = '1' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '0';
							WIDTH_STA		<= "10";
						else
							NULL;
						end if;
					else -- 10
						if ( WIDTHSIG = '0' ) then
							WIDTH_START		<= '1';
							WIDTH_END		<= '1';
							WIDTH_STA		<= "10";
						else
							NULL;
						end if;
					end if;
				when others =>
					NULL;
			end case;
		else
			NULL;
		end if;
		
	end process;
	
END RTL;