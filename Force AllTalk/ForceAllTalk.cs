using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Core;

namespace Force_AllTalk
{
    public class ForceAllTalkConfig : BasePluginConfig
    {
        public bool GeneralConfig { get; set; } = true;
    }
    public class ForceAllTalk : BasePlugin, IPluginConfig<ForceAllTalkConfig>
    {
        public override string ModuleName => "Force AllTalk";
        public override string ModuleAuthor => "Fortis (fork by Глеб Хлебов)";
        public override string ModuleDescription => "Enables all convar variables";
        public override string ModuleVersion => "1.2";

        private ConVar? _svAllTalk;
        private ConVar? _svDeadTalk;
        private ConVar? _svFullAllTalk;
        private ConVar? _svTalkEnemyDead;
        private ConVar? _svTalkEnemyLiving;

        public ForceAllTalkConfig Config { get; set; } = new();

        public void OnConfigParsed(ForceAllTalkConfig config)
        {
            Config = config;
        }

        public override void Load(bool hotReload)
        {
            if (!Config.GeneralConfig)
            {
                Console.WriteLine("[ForceAllTalk] Plugin not enabled!");
                return;
            }

            _svAllTalk = ConVar.Find("sv_alltalk");
            _svDeadTalk = ConVar.Find("sv_deadtalk");
            _svFullAllTalk = ConVar.Find("sv_full_alltalk");
            _svTalkEnemyDead = ConVar.Find("sv_talk_enemy_dead");
            _svTalkEnemyLiving = ConVar.Find("sv_talk_enemy_living");

            RegisterEventHandler<EventRoundStart>(OnRoundStart);
        }

        private HookResult OnRoundStart(EventRoundStart @event, GameEventInfo info)
        {
            if (!Config.GeneralConfig) return HookResult.Continue;

            _svAllTalk?.SetValue(true);
            _svDeadTalk?.SetValue(true);
            _svFullAllTalk?.SetValue(true);
            _svTalkEnemyDead?.SetValue(true);
            _svTalkEnemyLiving?.SetValue(true);

            return HookResult.Continue;
        }
    }
}