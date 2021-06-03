LIBRARY ieee;
USE ieee.std_logic_1164.all;
USE ieee.std_logic_unsigned.all;
use IEEE.std_logic_arith.all;

ENTITY CK_GEN IS
-- {{ALTERA_IO_BEGIN}} DO NOT REMOVE THIS LINE!
PORT
(
	RESET		:	IN		STD_LOGIC;
	RES		:	IN		STD_LOGIC;
	CLK		:	IN		STD_LOGIC;
	START		:	IN		STD_LOGIC;
	COUNT		:	IN		STD_LOGIC_VECTOR ( 15 downto 0 );
	WIDTHX	:	IN		STD_LOGIC_VECTOR ( 15 downto 0 );
	UPX		:	IN		STD_LOGIC_VECTOR ( 15 downto 0 );
	DOWNX		:	IN		STD_LOGIC_VECTOR ( 15 downto 0 );
	STARTC	:	IN		STD_LOGIC_VECTOR ( 15 downto 0 );
	OUTEN		:	IN		STD_LOGIC;
	
	CKO		:	OUT	STD_LOGIC;
	COUNTNOW	:	OUT	STD_LOGIC_VECTOR ( 15 downto 0 );
	fEND		:	OUT	STD_LOGIC;
	GENCOUNT	:	OUT	STD_LOGIC_VECTOR ( 15 downto 0 )
);

-- {{ALTERA_IO_END}} DO NOT REMOVE THIS LINE!
END CK_GEN;


--  Architecture Body

ARCHITECTURE RTL OF CK_GEN IS

	signal CountN	: std_logic_vector ( 15 downto 0 );
	signal GCount	: std_logic_vector ( 15 downto 0 );
	signal CKOUT	: std_logic;
	signal EOF		: std_logic;

BEGIN

	COUNTNOW <= CountN;
	GENCOUNT <= GCount;
	CKO		<= CKOUT;
	fEND		<= EOF;

	process (
		RESET,
		RES,
		CLK,
		START,
		OUTEN
	)
	begin
		if ( RESET = '0' or RES = '1' ) then
			CountN	<= ( others =>'0' );
			GCount	<= ( others =>'0' );
			CKOUT		<= '0';
			EOF		<= '0';
			
		elsif ( OUTEN = '0' ) then
			NULL;
			
		elsif ( CLK'event and CLK = '1' ) then
		
			if ( START = '1' ) then
				if ( EOF = '0' ) then
				-- main
					if ( 	
							( COUNT = X"FFFF" )										-- 無限
							or
							( ( COUNT /= X"FFFF" ) and ( CountN < COUNT ) )	-- 有限
						) then
						
						GCount <= GCount + '1';
						
						if ( GCount >= ( WidthX - 1 ) ) then
							GCount <= (others=>'0');
							COUNTN <= COUNTN + '1';
							
						elsif ( GCount >= DOWNX ) then
							CKOUT <= '0';
							
						elsif ( GCount >= UPx ) then
							if ( CountN >= STARTC ) then		-- UPを周期で遅らせる対応
								CKOUT <= '1';
							else
								CKOUT <= '0';
							end if;
							
						else
							NULL;
							
						end if;
						
					else
					-- END --
						CKOUT <= '0';
						EOF	<= '1';
				
					end if;
				-- main
				else
					CountN	<= ( others=>'0' );
					GCount	<= ( others=>'0' );
				end if;
			else
				CountN	<= ( others=>'0' );
				GCount	<= ( others=>'0' );
				CKOUT		<= '0';
				EOF		<= '0';
			
			end if;
		else
			NULL;
		end if;
	end process;

	
END RTL;