import pandas as pd
import json
from collections import defaultdict

def csv_to_skills_json():
    # 固定文件名
    csv_filename = "Skills.csv"
    json_filename = "Skills.json"
    
    try:
        # 尝试使用多种编码读取CSV文件
        encodings = ['utf-8', 'gbk', 'gb2312', 'gb18030', 'windows-1252', 'latin1']
        df = None
        
        for encoding in encodings:
            try:
                print(f"尝试使用 {encoding} 编码读取文件...")
                df = pd.read_csv(csv_filename, encoding=encoding)
                print(f"成功使用 {encoding} 编码读取文件")
                break
            except UnicodeDecodeError:
                continue
        
        if df is None:
            print("无法读取文件，请尝试手动指定正确的编码")
            return
    
        # 初始化技能数据结构
        skills_data = {"Skills": []}
        
        # 存储已处理的技能ID
        processed_skills = set()
        
        # 存储每个技能的级别数据
        skill_levels = defaultdict(list)
        
        # 处理DataFrame中的每一行
        for _, row in df.iterrows():
            skill_id = str(row['ID'])
            
            # 处理EffectIDs和BuffIDs列表
            effect_ids = []
            if pd.notna(row['EffectIDs']) and row['EffectIDs']:
                effect_ids = [effect_id.strip() for effect_id in str(row['EffectIDs']).split(',') if effect_id.strip()]
            
            buff_ids = []
            if pd.notna(row['BuffIDs']) and row['BuffIDs']:
                buff_ids = [buff_id.strip() for buff_id in str(row['BuffIDs']).split(',') if buff_id.strip()]
            
            # 创建级别数据
            level_data = {
                "Level": int(row['Level']) if pd.notna(row['Level']) else 1,
                "CoolDown": int(row['CoolDown']) if pd.notna(row['CoolDown']) else 0
            }
            
            if effect_ids:
                level_data["EffectIDs"] = effect_ids
            if buff_ids:
                level_data["BuffIDs"] = buff_ids
                
            # 添加到相应技能的级别数据
            skill_levels[skill_id].append(level_data)
            
            # 如果技能尚未处理，创建技能记录
            if skill_id not in processed_skills:
                skill_entry = {
                    "ID": skill_id,
                    "Name": row['Name'],
                    "SkillType": row['SkillType'],
                    "Description": row['Description'],
                    "Target": row['Target'],
                    "TargetAmount": int(row['TargetAmount']) if pd.notna(row['TargetAmount']) else 1
                }
                
                skills_data["Skills"].append(skill_entry)
                processed_skills.add(skill_id)
        
        # 为每个技能填充级别数据
        for skill in skills_data["Skills"]:
            skill_id = skill["ID"]
            if skill_id in skill_levels:
                skill["LevelsData"] = skill_levels[skill_id]
        
        # 输出到JSON文件
        with open(json_filename, 'w', encoding='utf-8') as jsonfile:
            json.dump(skills_data, jsonfile, ensure_ascii=False, indent=2)
        
        print(f"转换完成！数据已保存到 {json_filename}")
    
    except Exception as e:
        print(f"发生错误: {e}")
        import traceback
        traceback.print_exc()

# 直接执行功能
if __name__ == "__main__":
    csv_to_skills_json()