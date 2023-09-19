using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP_Butler
{
    public interface ICRUD
    {
        // Add records
        Task AddRecordShips(ShipsToKill ship);
        Task AddRecordMiners(AutominerLocation miner);
        Task AddRecordDronite(DroniteField dronite);
        Task AddRecordPOI(POISpot poi);
        Task AddRecordTraders(TraderLocation trader);

        // Remove Records
        Task RemoveRecordShips(ShipsToKill ship);
        Task RemoveRecordMiners(AutominerLocation miner);
        Task RemoveRecordDronite(DroniteField dronite);
        Task RemoveRecordPOI(POISpot poi);
        Task RemoveRecordTraders(TraderLocation trader);

        // Update records
        Task UpdateRecordShips(decimal ID, ShipsToKill ship);
        Task UpdateRecordMiners(decimal ID, AutominerLocation miner);
        Task UpdateRecordDronite(decimal ID, DroniteField dronite);
        Task UpdateRecordPOI(decimal ID, POISpot poi);
        Task UpdateRecordTraders(decimal ID, TraderLocation trader);

        // Get records
        Task<ICollection<ShipsToKill>> GetAllShips();
        ICollection<AutominerLocation> GetAllMiners();
        ICollection<DroniteField> GetAllDronite();
        ICollection<POISpot> GetAllPOI();
        ICollection<TraderLocation> GetAllTraders();

        // ID Autogeneration logic
        int GetMaxShips();
        int GetMaxMiner();
        int GetMaxDronite();
        int GetMaxPOI();
        int GetMaxTrader();
    }

    public class CRUD : ICRUD
    {
        Entities entities;

        public CRUD()
        {
            entities = new Entities();
        }
        public int GetMaxShips()
        {
            return Convert.ToInt32(entities.ShipsToKills.Max(p => p.ID));
        }
        public int GetMaxMiner()
        {
            return Convert.ToInt32(entities.AutominerLocations.Max(p => p.ID));
        }
        public int GetMaxDronite()
        {
            return Convert.ToInt32(entities.DroniteFields.Max(p => p.ID));
        }
        public int GetMaxPOI()
        {
            return Convert.ToInt32(entities.POISpots.Max(p => p.ID));
        }
        public int GetMaxTrader()
        {
            return Convert.ToInt32(entities.TraderLocations.Max(p => p.ID));
        }

        public async Task AddRecordShips(ShipsToKill ship)
        {
            entities.ShipsToKills.Add(ship);
            await entities.SaveChangesAsync();
        }

        public async Task AddRecordMiners(AutominerLocation miner)
        {
            entities.AutominerLocations.Add(miner);
            await entities.SaveChangesAsync();
        }

        public async Task AddRecordDronite(DroniteField dronite)
        {
            entities.DroniteFields.Add(dronite);
            await entities.SaveChangesAsync();
        }
        public async Task AddRecordPOI(POISpot poi)
        {
            entities.POISpots.Add(poi);
            await entities.SaveChangesAsync();
        }
        public async Task AddRecordTraders(TraderLocation trader)
        {
            entities.TraderLocations.Add(trader);
            await entities.SaveChangesAsync();
        }

        public async Task RemoveRecordShips(ShipsToKill ship)
        {
            entities.ShipsToKills.Remove(ship);
            await entities.SaveChangesAsync();
        }

        public async Task RemoveRecordMiners(AutominerLocation miner)
        {
            entities.AutominerLocations.Remove(miner);
            await entities.SaveChangesAsync();
        }

        public async Task RemoveRecordDronite(DroniteField dronite)
        {
            entities.DroniteFields.Remove(dronite);
            await entities.SaveChangesAsync();
        }

        public async Task RemoveRecordPOI(POISpot poi)
        {
            entities.POISpots.Remove(poi);
            await entities.SaveChangesAsync();
        }

        public async Task RemoveRecordTraders(TraderLocation trader)
        {
            entities.TraderLocations.Remove(trader);
            await entities.SaveChangesAsync();
        }

        public async Task UpdateRecordShips(decimal ID, ShipsToKill ship)
        {
            var shipToUpdate = entities.ShipsToKills.Find(ID);
            shipToUpdate.ID = ship.ID;
            shipToUpdate.Sector = ship.Sector;
            shipToUpdate.SolarSystem = ship.SolarSystem;
            shipToUpdate.ShipName = ship.ShipName;
            await entities.SaveChangesAsync();
        }

        public async Task UpdateRecordMiners(decimal ID, AutominerLocation miner)
        {
            var minterToUpdate = entities.AutominerLocations.Find(ID);
            minterToUpdate.ID = miner.ID;
            minterToUpdate.SolarSystem = miner.SolarSystem;
            minterToUpdate.Planet = miner.Planet;
            await entities.SaveChangesAsync();
        }

        public async Task UpdateRecordDronite(decimal ID, DroniteField dronite)
        {
            var droniteToUpdate = entities.DroniteFields.Find(ID);
            droniteToUpdate.ID = dronite.ID;
            droniteToUpdate.Sector = dronite.Sector;
            droniteToUpdate.SolarSystem= dronite.SolarSystem;
            droniteToUpdate.IsCleared = dronite.IsCleared;
            await entities.SaveChangesAsync();
        }

        public async Task UpdateRecordPOI(decimal ID, POISpot poi)
        {
            var poiToUpdate = entities.POISpots.Find(ID);
            poiToUpdate.ID = poi.ID;
            poiToUpdate.Sector = poi.Sector;
            poiToUpdate.SolarSystem = poi.SolarSystem;
            poiToUpdate.Name = poi.Name;
            poiToUpdate.OnPlanet = poi.OnPlanet;
            poiToUpdate.Planet = poi.Planet;
            await entities.SaveChangesAsync();
        }

        public async Task UpdateRecordTraders(decimal ID, TraderLocation trader)
        {
            var tradersToUpdate = entities.TraderLocations.Find(ID);
            tradersToUpdate.ID = trader.ID;
            tradersToUpdate.TraderName = trader.TraderName;
            tradersToUpdate.SolarSystem = trader.SolarSystem;
            tradersToUpdate.Sector = trader.Sector;
            tradersToUpdate.HasPlanet = trader.HasPlanet;
            tradersToUpdate.PlanetName = trader.PlanetName;
            await entities.SaveChangesAsync();
        }

        public async Task<ICollection<ShipsToKill>> GetAllShips()
        {
            return await Task.FromResult(entities.ShipsToKills.ToList());
        }

        public ICollection<AutominerLocation> GetAllMiners()
        {
            return entities.AutominerLocations.ToList();
        }

        public ICollection<DroniteField> GetAllDronite()
        {
            return entities.DroniteFields.ToList();
        }

        public ICollection<POISpot> GetAllPOI()
        {
            return entities.POISpots.ToList();
        }

        public ICollection<TraderLocation> GetAllTraders()
        {
            return entities.TraderLocations.ToList();
        }
    }
}
