local out = print

function FromTiledToLevel(T)
	local tileSize = T.tilewidth
	for layerId, layerData in ipairs(T.layers) do
		
		if layerData.type == 'tilelayer' then
		
			if layerData.name == 'Walls' then
				out ":static"
			elseif layerData.name == 'Props' then
				out ":props"
			end
			
			for _, chunck in ipairs(layerData.chunks) do
			
				for i, tile in ipairs(chunck.data) do
					if tile ~= 0 then
						local xPos = (i - 1) % 16 + chunck.x
						local yPos = math.floor((i - 1) / 16) + chunck.y
					
						out(("%d %s %s"):format(tile - 1, xPos * tileSize, yPos * tileSize))
					end
				end
			end
		
		elseif layerData.type == 'objectgroup' then
			out ":beacons"
			
			for i, obj in ipairs(layerData.objects) do
				out(("%s %s %s"):format(obj.name, obj.x, obj.y))
			end
		end
	end
end