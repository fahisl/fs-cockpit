local default_output_file = nil

function LuaExportStart()
	package.path  = package.path..";"..lfs.currentdir().."/LuaSocket/?.lua"
	package.cpath = package.cpath..";"..lfs.currentdir().."/LuaSocket/?.dll"
	socket = require("socket")
	host = host or "localhost"
	port = port or 3333
	c = socket.try(socket.connect(host, port)) -- connect to the listener socket
	c:setoption("tcp-nodelay",true) -- set immediate transmission mode
	local version = LoGetVersionInfo() --request current version info (as it showed by Windows Explorer fo DCS.exe properties)
	if version and default_output_file then
		default_output_file:write("ProductName: "..version.ProductName..'\n')
		default_output_file:write(string.format("FileVersion: %d.%d.%d.%d\n", version.FileVersion[1], version.FileVersion[2], version.FileVersion[3], version.FileVersion[4]))
		default_output_file:write(string.format("ProductVersion: %d.%d.%d.%d\n", version.ProductVersion[1], version.ProductVersion[2], version.ProductVersion[3], version.ProductVersion[4]))
	end

end

function LuaExportBeforeNextFrame()
end

function LuaExportAfterNextFrame()
	local pitch, bank, yaw = LoGetADIPitchBankYaw()
	socket.try(c:send(string.format("%+06d%+06d\n", -5730*pitch, -5730*bank)))
end

function LuaExportStop()
   if default_output_file then
	  default_output_file:close()
	  default_output_file = nil
   end
	socket.try(c:send("quit")) -- to close the listener socket
	c:close()
end

function LuaExportActivityNextEvent(t)
	local tNext = t
	return tNext
end
